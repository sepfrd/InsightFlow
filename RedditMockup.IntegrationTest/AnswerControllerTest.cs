using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RedditMockup.Common.Dtos;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RedditMockup.IntegrationTest;

public class AnswerControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    #region [Field(s)]

    private const string BaseAddress = "/api/Answer";

    private const string LoginAdress = "/api/Account/Login";

    private const string ValidTitle = "How to do sth";
    
    private const string ValidDescription = "Can anybody help me with my problem?";

    private readonly WebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    #endregion

    #region [Constructor]

    public AnswerControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));

        _client = _factory.CreateClient();
    }

    #endregion

    #region [Method(s)]

    private async Task AuthenticateAsync()
    {
        var loginDto = new LoginDto()
        {
            Username = "admin_admin",
            Password = "adminnnn",
            RememberMe = true
        };

        var serializedLoginDto = JsonConvert.SerializeObject(loginDto, Formatting.Indented);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        await _client.PostAsync(LoginAdress, stringContent);
    }


    #endregion

    #region [Theory Method(s)]

    [Theory]
    [MemberData(nameof(GenerateCreateData))]
    public async Task Create_ReturnExpectedResult(AnswerDto dto, TestResultCode testResultCode)
    {
        #region [Arrange]

        var serializedLoginDto = JsonSerializer.Serialize(dto);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        #endregion

        #region [Act]

        var response = await _client.PostAsync(BaseAddress, stringContent);

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        #endregion

        #region [Assert]



        #endregion

    }


    [Fact]
    public async Task GetAll_ReturnCustomResponseOfListOfAnswerDto()
    {
        #region [Act]

        var response = await _client.GetAsync(BaseAddress);

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() =>
            JsonConvert.DeserializeObject<CustomResponse<List<AnswerDto>>>(streamResponse));

        #endregion

        #region [Assert]

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse?.Data?.Should().BeOfType<List<AnswerDto>>();

        #endregion
    }

    [Theory]
    [MemberData(nameof(GenerateGetByIdData))]
    public async Task GetById_ReturnExpectedResult(int id, bool isAuthenticated, HttpStatusCode httpStatusCode)
    {
        if (isAuthenticated)
        {
            #region [Arrange]

            await AuthenticateAsync();

            #endregion

            #region [Act]

            var response = await _client.GetAsync(BaseAddress + "/id" + $"?id={id}");

            var streamResponse = await response.Content.ReadAsStreamAsync();

            var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

            #endregion

            #region [Assert]

            response.StatusCode.Should().Be(httpStatusCode);

            if (id < 10)
            {
                apiResponse?.IsSuccess.Should().BeTrue();
            }
            else
            {
                apiResponse?.IsSuccess.Should().BeFalse();
            }

            #endregion
        }
        else
        {
            #region [Act]

            var response = await _client.GetAsync(BaseAddress + "/id" + $"?id={id}");

            #endregion

            #region [Assert]

            response.StatusCode.Should().Be(httpStatusCode);

            #endregion
        }


    }

    [Fact]
    public async Task GetVotes_ReturnCustomResponseOfListOfVoteDto()
    {
        #region [Act]

        var response = await _client.GetAsync(BaseAddress + "/Votes");

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() =>
            JsonConvert.DeserializeObject<CustomResponse<List<VoteDto>>>(streamResponse));

        #endregion

        #region [Assert]

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse?.Data?.Should().BeOfType<List<VoteDto>>();

        #endregion
    }

    [Theory]
    [MemberData(nameof(GenerateSubmitVoteData))]
    public async Task SubmitVote_ReturnExpectedResult(int answerId, bool kind, TestResultCode testResultCode)
    {
        #region [Arrange]

        if (testResultCode != TestResultCode.Unauthorized)
        {
            await AuthenticateAsync();
        }

        #endregion

        #region [Act]

        var response = await _client.PostAsync(BaseAddress + $"/SubmitVote?answerId={answerId}&kind={kind}", null);

        if (testResultCode == TestResultCode.BadRequest)
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            return;
        }

        if (testResultCode == TestResultCode.Unauthorized)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            return;
        }

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<CustomResponse>(streamResponse);

        #endregion

        #region [Assert]

        switch (testResultCode)
        {
            case TestResultCode.OK:

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                apiResponse?.IsSuccess.Should().Be(true);

                break;

            case TestResultCode.NotFound:

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                apiResponse?.IsSuccess.Should().Be(false);

                break;

        }

        #endregion
    }

    #endregion

    #region [Data Method(s)]

    public static IEnumerable<object[]> GenerateCreateData()
    {
        return new List<object[]>
        {
            new object[]
            {
                new AnswerDto
                {
                    QuestionId = 5,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                HttpStatusCode.OK
            },

            new object[]
            {
                new AnswerDto
                {
                    QuestionId = 14,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                HttpStatusCode.NotFound
            },

            new object[]
            {
                new AnswerDto
                {
                    QuestionId = 5,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                HttpStatusCode.OK
            },

            new object[]
            {
                new AnswerDto
                {
                    QuestionId = 5,
                    Title = ValidTitle,
                    Description = ValidDescription
                },
                HttpStatusCode.OK
            }
        };
    }

    public static IEnumerable<object[]> GenerateGetByIdData()
    {
        return new List<object[]>
        {
            new object[]
            {
                1,
                true,
                HttpStatusCode.OK
            },
            new object[]
            {
                20,
                true,
                HttpStatusCode.OK
            },
            new object[]
            {
                2,
                false,
                HttpStatusCode.Unauthorized
            },
            new object[]
            {
                20,
                false,
                HttpStatusCode.Unauthorized
            }
        };
    }

    public static IEnumerable<object[]> GenerateSubmitVoteData()
    {
        return new List<object[]>
        {
            new object[]
            {
                5,
                true,
                TestResultCode.OK
            },

            new object[]
            {
                5,
                false,
                TestResultCode.Unauthorized
            },

            new object[]
            {
                20,
                true,
                TestResultCode.NotFound
            }

        };
    }

    #endregion

    public enum TestResultCode
    {
        OK,
        NotFound,
        BadRequest,
        Unauthorized,
        Forbidden
    }
}