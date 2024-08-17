using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FluentAssertions;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Helpers;
using InsightFlow.IntegrationTests.Common;
using InsightFlow.IntegrationTests.Common.AuthMockHelpers;
using InsightFlow.IntegrationTests.Common.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace InsightFlow.IntegrationTests;

public class AnswerControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string ValidTitle = "How to do sth";
    private const string ValidDescription = "Can anybody help me with my problem?";

    private static readonly Guid _validAnswerGuid = FakeDataHelper.FakeAnswer.Guid;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AnswerControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        Utilities.ResetDatabase(_factory);
    }

    private HttpClient GetHttpClient(string? role = null)
    {
        HttpClient client;

        if (!string.IsNullOrEmpty(role))
        {
            client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services
                            .AddAuthentication(Constants.TestAuthSchemeName)
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                                Constants.TestAuthSchemeName,
                                _ => { });

                        var serviceProvider = services.BuildServiceProvider();

                        services
                            .AddSingleton(new TestAuthHandler(
                                serviceProvider.GetRequiredService<IOptionsMonitor<AuthenticationSchemeOptions>>(),
                                serviceProvider.GetRequiredService<ILoggerFactory>(),
                                serviceProvider.GetRequiredService<UrlEncoder>(),
                                role))
                            .AddTransient<IAuthenticationSchemeProvider, TestAuthSchemeProvider>();
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }
        else
        {
            client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        return client;
    }

    [Theory]
    [MemberData(nameof(CreateTestData))]
    public async Task Create_ReturnExpectedResult(CreateAnswerRequestDto createAnswerRequestDto)
    {
        var serializedLoginDto = JsonSerializer.Serialize(createAnswerRequestDto.AnswerDto);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        var client = GetHttpClient(createAnswerRequestDto.Role);

        var response = await client.PostAsync(Constants.AnswersBaseAddress, stringContent);

        if (createAnswerRequestDto.ResultStatusCode == HttpStatusCode.Forbidden)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            return;
        }

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse<AnswerDto?>>(streamResponse);

        response.StatusCode.Should().Be(createAnswerRequestDto.ResultStatusCode);

        if (createAnswerRequestDto.ResultStatusCode == HttpStatusCode.Created)
        {
            apiResponse?.IsSuccess.Should().BeTrue();
        }
        else if (createAnswerRequestDto.ResultStatusCode == HttpStatusCode.NotFound)
        {
            apiResponse?.IsSuccess.Should().BeFalse();
        }
        else
        {
            Assert.NotNull(null);
        }
    }

    [Fact]
    public async Task GetAll_ReturnCustomResponseOfListOfAnswerDto()
    {
        var client = GetHttpClient();

        var response = await client.GetAsync(Constants.AnswersBaseAddress);

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() =>
            JsonConvert.DeserializeObject<CustomResponse<List<AnswerDto>>>(streamResponse));

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse?.Data?.Should().BeOfType<List<AnswerDto>>();
    }

    [Theory]
    [MemberData(nameof(GetAnswerByGuidTestData))]
    public async Task GetByGuid_ReturnExpectedResult(GetAnswerByGuidRequestDto getAnswerByGuidRequestDto)
    {
        var client = GetHttpClient(getAnswerByGuidRequestDto.Role);

        var response = await client.GetAsync(Constants.AnswersBaseAddress + "/guid/" + getAnswerByGuidRequestDto.AnswerGuid);

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse<AnswerDto?>>(streamResponse);

        response.StatusCode.Should().Be(getAnswerByGuidRequestDto.ResponseStatusCode);

        if (getAnswerByGuidRequestDto.AnswerGuid != _validAnswerGuid)
        {
            apiResponse?.IsSuccess.Should().BeFalse();
        }
        else
        {
            apiResponse?.IsSuccess.Should().BeTrue();
        }
    }

    public static TheoryData<CreateAnswerRequestDto> CreateTestData() =>
        new()
        {
            new CreateAnswerRequestDto
            {
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = FakeDataHelper.FakeQuestion.Guid,
                    UserGuid = FakeDataHelper.FakeUser.Guid,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                ResultStatusCode = HttpStatusCode.Created,
                Role = ApplicationConstants.UserRoleName
            },
            new CreateAnswerRequestDto
            {
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    UserGuid = FakeDataHelper.FakeUser.Guid,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                ResultStatusCode = HttpStatusCode.NotFound,
                Role = ApplicationConstants.UserRoleName
            },
            new CreateAnswerRequestDto
            {
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = FakeDataHelper.FakeQuestion.Guid,
                    UserGuid = FakeDataHelper.FakeAdmin.Guid,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                ResultStatusCode = HttpStatusCode.Forbidden,
                Role = ApplicationConstants.AdminRoleName
            }
        };

    public static TheoryData<GetAnswerByGuidRequestDto> GetAnswerByGuidTestData() =>
        new()
        {
            new GetAnswerByGuidRequestDto
            {
                AnswerGuid = _validAnswerGuid,
                ResponseStatusCode = HttpStatusCode.OK
            },
            new GetAnswerByGuidRequestDto
            {
                AnswerGuid = Guid.NewGuid(),
                ResponseStatusCode = HttpStatusCode.NotFound
            }
        };
}