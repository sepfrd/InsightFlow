using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RedditMockup.IntegrationTest;

public class AccountApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    #region [Field(s)]

    private const string BaseAddress = "/api/Account";

    private readonly WebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    #endregion

    #region [Constructor]

    public AccountApiTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));

        _client = _factory.CreateClient();
    }

    #endregion

    #region [Method(s)]

    private async Task AuthenticateAsync()
    {
        var _loginDto = new LoginDto()
        {
            Username = "sepehr_frd",
            Password = "sfr1376",
            RememberMe = true
        };

        var serializedLoginDto = JsonConvert.SerializeObject(_loginDto, Formatting.Indented);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        await _client.PostAsync($"{BaseAddress}/Login", stringContent);
    }

    #endregion

    #region [Theory Method(s)]

    [Fact]
    public async Task GetAll_ReturnExpectedResult()
    {
        #region [Arrange]

        await AuthenticateAsync();

        #endregion

        #region [Act]

        var response = await _client.GetAsync(BaseAddress);

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<UserViewModel>>(streamResponse));

        #endregion

        #region [Assert]

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse.Should().BeOfType<List<UserViewModel>>();

        #endregion
    }

    [Theory]
    [MemberData(nameof(GenerateLoginData))]
    public async Task Login_ReturnExpectedResult(LoginDto loginDto, bool expected)
    {
        #region [Arrange]

        var serializedLoginDto = JsonSerializer.Serialize(loginDto);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        #endregion

        #region [Act]

        var response = await _client.PostAsync($"{BaseAddress}/Login", stringContent);

        var streamResponse = await response.Content.ReadAsStreamAsync();

        var apiResponse = await JsonSerializer.DeserializeAsync<SamanSalamatResponse>(streamResponse);

        #endregion

        #region [Assert]

        apiResponse?.IsSuccess.Should().Be(expected);

        #endregion
    }

    [Theory]
    [MemberData(nameof(GenerateIntegrationData))]
    public async Task LoginGetAllLogout_ReturnExpectedResult(LoginDto loginDto, TestResultCode expected)
    {
        #region [Arrange]

        var serializedLoginDto = JsonSerializer.Serialize(loginDto);

        var loginDtoStringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        #endregion

        #region [Act]

        #region [Login]

        var loginResponse = await _client.PostAsync($"{BaseAddress}/Login", loginDtoStringContent);

        var loginStreamResponse = await loginResponse.Content.ReadAsStreamAsync();

        var loginApiResponse = await JsonSerializer.DeserializeAsync<SamanSalamatResponse>(loginStreamResponse);

        #endregion

        #region [GetAll]

        var getAllResponse = await _client.GetAsync(BaseAddress);

        #endregion

        #region [Logout]

        var logoutResponse = await _client.GetAsync($"{BaseAddress}/Logout");

        var logoutStreamResponse = await logoutResponse.Content.ReadAsStreamAsync();

        var logoutApiResponse = await JsonSerializer.DeserializeAsync<SamanSalamatResponse>(logoutStreamResponse);

        #endregion

        #endregion

        #region [Assert]

        switch (expected)
        {
            case TestResultCode.AllFailed:

                loginApiResponse?.IsSuccess.Should().BeFalse();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

                logoutApiResponse?.IsSuccess.Should().BeFalse();

                break;

            case TestResultCode.AllSuccessful:

                loginApiResponse?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                logoutApiResponse?.IsSuccess.Should().BeTrue();

                break;

            default:
                Assert.True(false);
                break;
        }

        #endregion
    }

    #endregion

    #region [Data Method(s)]

    public static IEnumerable<object[]> GenerateLoginData()
    {
        yield return new object[]
        {
            new LoginDto { Username = "sepehr_frd", Password = "sfr1376", RememberMe = false }, true
        };

        yield return new object[]
        {
            new LoginDto { Username = "admin_admin", Password = "adminnnn", RememberMe = false }, true
        };

        yield return new object[]
        {
            new LoginDto { Username = "sepehr_frd", Password = "asdasdasdasd", RememberMe = false }, false
        };

        yield return new object[]
        {
            new LoginDto { Username = "sepehr_d", Password = "sfr1376", RememberMe = false }, false
        };

        yield return new object[] { new LoginDto { Username = "223", Password = "sd2", RememberMe = false }, false };
    }

    public static IEnumerable<object[]> GenerateIntegrationData()
    {
        yield return new object[]
        {
            new LoginDto { Username = "sepehr_frd", Password = "sfr1376", RememberMe = true },
            TestResultCode.AllSuccessful
        };

        yield return new object[]
        {
            new LoginDto { Username = "admin_admin", Password = "adminnnn", RememberMe = true },
            TestResultCode.AllSuccessful
        };

        yield return new object[]
        {
            new LoginDto { Username = "sepehr_frd", Password = "sfr1231123376", RememberMe = true },
            TestResultCode.AllFailed
        };

        yield return new object[]
        {
            new LoginDto { Username = "sepeasdfahr_frd", Password = "sfr1376", RememberMe = true },
            TestResultCode.AllFailed
        };
    }

    #endregion

    public enum TestResultCode
    {
        AllFailed,
        AllSuccessful
    }
}