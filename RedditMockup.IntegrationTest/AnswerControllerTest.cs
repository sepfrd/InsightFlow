using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedditMockup.Common.Dtos;
using RedditMockup.IntegrationTest.Common;
using RedditMockup.IntegrationTest.Common.Dtos;
using RedditMockup.IntegrationTest.Common.Enums;
using RedditMockup.IntegrationTest.Handlers;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RedditMockup.IntegrationTest;

public class AnswerControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string ValidTitle = "How to do sth";
    private const string ValidDescription = "Can anybody help me with my problem?";
    private readonly Guid _validAnswerGuid;

    private readonly CustomWebApplicationFactory<Program> _factory;

    public AnswerControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        Utilities.ResetDatabase(_factory);

        _validAnswerGuid = Utilities.GetValidAnswerGuid(_factory);
    }

    private HttpClient GetHttpClient(bool isAuthenticated)
    {
        HttpClient client;

        if (isAuthenticated)
        {
            client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddAuthentication(defaultScheme: Constants.TestAuthSchemeName)
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                                Constants.TestAuthSchemeName, _ => { });
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });
        }
        else
        {
            client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
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

        var client = GetHttpClient(createAnswerRequestDto.TestResult != AnswerControllerTestResult.Unauthorized);

        var response = await client.PostAsync(Constants.PublicAnswersBaseAddress, stringContent);

        if (createAnswerRequestDto.TestResult == AnswerControllerTestResult.Unauthorized)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            return;
        }

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        if (createAnswerRequestDto.TestResult == AnswerControllerTestResult.OK)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeTrue();
        }
        else if (createAnswerRequestDto.TestResult == AnswerControllerTestResult.NotFound)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeFalse();
        }
        else
        {
            Assert.Null("Error");
        }
    }

    [Fact]
    public async Task GetAll_ReturnCustomResponseOfListOfAnswerDto()
    {
        var client = GetHttpClient(false);

        var response = await client.GetAsync(Constants.PublicAnswersBaseAddress);

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() =>
            JsonConvert.DeserializeObject<CustomResponse<List<AnswerDto>>>(streamResponse));

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse?.Data?.Should().BeOfType<List<AnswerDto>>();
    }

    [Theory]
    [MemberData(nameof(GetAnswerByIdTestData))]
    public async Task GetById_ReturnExpectedResult(GetAnswerByIdRequestDto getAnswerByIdRequestDto)
    {
        var client = GetHttpClient(getAnswerByIdRequestDto.IsAuthenticated);

        var response = await client.GetAsync(Constants.PublicAnswersBaseAddress + "/id" + $"?id={getAnswerByIdRequestDto.AnswerId}");

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        if (getAnswerByIdRequestDto.IsAuthenticated)
        {
            response.StatusCode.Should().Be(getAnswerByIdRequestDto.ResponseStatusCode);

            if (getAnswerByIdRequestDto.AnswerId > 150)
            {
                apiResponse?.IsSuccess.Should().BeFalse();
            }
            else
            {
                apiResponse?.IsSuccess.Should().BeTrue();
            }
        }
        else
        {
            apiResponse?.IsSuccess.Should().BeFalse();
        }
    }

    [Theory]
    [MemberData(nameof(UpdateAnswerTestData))]
    public async Task Update_ReturnExpectedResult(UpdateAnswerRequestDto updateAnswerRequestDto)
    {
        var serializedLoginDto = JsonSerializer.Serialize(updateAnswerRequestDto.AnswerDto);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        var client = GetHttpClient(updateAnswerRequestDto.TestResult != AnswerControllerTestResult.Unauthorized);

        var response = await client.PutAsync(
            $"{Constants.PublicAnswersBaseAddress}?id={updateAnswerRequestDto.AnswerId}",
            stringContent);

        if (updateAnswerRequestDto.TestResult == AnswerControllerTestResult.Unauthorized)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            return;
        }

        var streamResponse = await response.Content.ReadAsStreamAsync();
        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        if (updateAnswerRequestDto.TestResult == AnswerControllerTestResult.OK)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeTrue();
        }
        else if (updateAnswerRequestDto.TestResult == AnswerControllerTestResult.NotFound)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeFalse();
        }
        else
        {
            Assert.Null("Error");
        }
    }

    [Fact]
    public async Task GetVotes_ReturnCustomResponseOfListOfVoteDto()
    {
        var client = GetHttpClient(false);

        var response = await client.GetAsync(Constants.PublicAnswersBaseAddress + "/guid" + _validAnswerGuid + "/votes");

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() =>
            JsonConvert.DeserializeObject<CustomResponse<List<VoteDto>>>(streamResponse));

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse?.Data?.Should().BeOfType<List<VoteDto>>();
    }

    [Theory]
    [MemberData(nameof(SubmitAnswerVoteTestData))]
    public async Task SubmitVote_ReturnExpectedResult(SubmitAnswerRequestDto submitAnswerRequestDto)
    {
        var client = GetHttpClient(submitAnswerRequestDto.TestResult != AnswerControllerTestResult.Unauthorized);

        var jsonData = JsonSerializer.Serialize(submitAnswerRequestDto.VoteKind);

        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await client
            .PostAsync($"{Constants.PublicAnswersBaseAddress}/guid/{_validAnswerGuid}/votes", content);

        if (submitAnswerRequestDto.TestResult == AnswerControllerTestResult.Unauthorized)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            return;
        }

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        if (submitAnswerRequestDto.TestResult == AnswerControllerTestResult.OK)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeTrue();
        }
        else if (submitAnswerRequestDto.TestResult == AnswerControllerTestResult.NotFound)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeFalse();
        }
        else
        {
            Assert.Null("Error");
        }
    }

    [Theory]
    [MemberData(nameof(DeleteAnswerTestData))]
    public async Task Delete_ReturnExpectedResult(DeleteAnswerRequestDto deleteAnswerRequestDto)
    {
        var client = GetHttpClient(deleteAnswerRequestDto.TestResult != AnswerControllerTestResult.Unauthorized);

        var response = await client.DeleteAsync($"{Constants.PublicAnswersBaseAddress}/guid/{_validAnswerGuid}");

        if (deleteAnswerRequestDto.TestResult == AnswerControllerTestResult.Unauthorized)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            return;
        }

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        if (deleteAnswerRequestDto.TestResult == AnswerControllerTestResult.OK)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeTrue();
        }
        else if (deleteAnswerRequestDto.TestResult == AnswerControllerTestResult.NotFound)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            apiResponse?.IsSuccess.Should().BeFalse();
        }
        else
        {
            Assert.Null("Error");
        }
    }

    public static TheoryData<CreateAnswerRequestDto> CreateTestData() =>
        new()
        {
            new CreateAnswerRequestDto
            {
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.OK
            },
            new CreateAnswerRequestDto
            {
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.NotFound
            },
            new CreateAnswerRequestDto
            {
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.Unauthorized
            }
        };

    public static TheoryData<GetAnswerByIdRequestDto> GetAnswerByIdTestData() =>
        new()
        {
            new GetAnswerByIdRequestDto
            {
                AnswerId = 1,
                IsAuthenticated = true,
                ResponseStatusCode = HttpStatusCode.OK
            },
            new GetAnswerByIdRequestDto
            {
                AnswerId = 20,
                IsAuthenticated = true,
                ResponseStatusCode = HttpStatusCode.OK
            },
            new GetAnswerByIdRequestDto
            {
                AnswerId = 2,
                IsAuthenticated = false,
                ResponseStatusCode = HttpStatusCode.Unauthorized
            },
            new GetAnswerByIdRequestDto
            {
                AnswerId = 20,
                IsAuthenticated = false,
                ResponseStatusCode = HttpStatusCode.Unauthorized
            }
        };

    public static TheoryData<SubmitAnswerRequestDto> SubmitAnswerVoteTestData() =>
        new()
        {
            new SubmitAnswerRequestDto
            {
                AnswerId = 5,
                VoteKind = true,
                TestResult = AnswerControllerTestResult.OK
            },
            new SubmitAnswerRequestDto
            {
                AnswerId = 5,
                VoteKind = false,
                TestResult = AnswerControllerTestResult.Unauthorized
            },
            new SubmitAnswerRequestDto
            {
                AnswerId = 200,
                VoteKind = true,
                TestResult = AnswerControllerTestResult.NotFound
            }
        };

    public static TheoryData<UpdateAnswerRequestDto> UpdateAnswerTestData() =>
        new()
        {
            new UpdateAnswerRequestDto
            {
                AnswerId = 5,
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.OK
            },
            new UpdateAnswerRequestDto
            {
                AnswerId = 5,
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.Unauthorized
            },
            new UpdateAnswerRequestDto
            {
                AnswerId = 5,
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.NotFound
            },
            new UpdateAnswerRequestDto
            {
                AnswerId = 450,
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.NotFound
            },
            new UpdateAnswerRequestDto
            {
                AnswerId = 450,
                AnswerDto = new AnswerDto
                {
                    QuestionGuid = Guid.NewGuid(),
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                TestResult = AnswerControllerTestResult.NotFound
            }
        };

    public static TheoryData<DeleteAnswerRequestDto> DeleteAnswerTestData() =>
        new()
        {
            new DeleteAnswerRequestDto
            {
                AnswerId = 5,
                TestResult = AnswerControllerTestResult.OK
            },
            new DeleteAnswerRequestDto
            {
                AnswerId = 200,
                TestResult = AnswerControllerTestResult.NotFound
            },
            new DeleteAnswerRequestDto
            {
                AnswerId = 5,
                TestResult = AnswerControllerTestResult.Unauthorized
            }
        };
}