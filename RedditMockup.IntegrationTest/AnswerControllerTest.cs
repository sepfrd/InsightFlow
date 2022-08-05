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
    
    #endregion

    #region [Data Method(s)]

    public static IEnumerable<object[]> GenerateGetByIdData()
    {
        yield return new object[]
        {
            1,
            true,
            HttpStatusCode.OK
        };
        
        yield return new object[]
        {
            20,
            true,
            HttpStatusCode.OK
        };
        
        yield return new object[]
        {
            2,
            false,
            HttpStatusCode.Unauthorized
        };
        
        yield return new object[]
        {
            20,
            false,
            HttpStatusCode.Unauthorized
        };
    }
    
    #endregion
}