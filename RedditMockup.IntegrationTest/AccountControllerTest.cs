using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RedditMockup.Common.Dtos;
using RedditMockup.IntegrationTest.Common;
using RedditMockup.IntegrationTest.Common.Dtos;
using RedditMockup.IntegrationTest.Common.Enums;
using RestSharp;
using Xunit;

namespace RedditMockup.IntegrationTest;

public class AccountControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const int DefaultTimeout = 2000;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AccountControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        Utilities.ResetDatabase(_factory);
    }

    [Theory]
    [MemberData(nameof(LoginTestData))]
    public async Task Login_ReturnExpectedResult(LoginRequestDto loginRequestDto)
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var restClient = new RestClient(client);

        var request = new RestRequest($"{Constants.AuthBaseAddress}/login")
        {
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeout)
        };

        request.AddJsonBody(loginRequestDto.LoginDto);

        var response = await restClient.ExecutePostAsync<CustomResponse>(request);

        response.Data?.IsSuccess.Should().Be(loginRequestDto.ShouldSucceed);
    }

    [Theory]
    [MemberData(nameof(LoginGetAllLogoutTestData))]
    public async Task LoginGetAllLogout_ReturnExpectedResult(LoginGetAllLogoutRequestDto loginRequestDto)
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var restClient = new RestClient(client);

        var loginRequest = new RestRequest($"{Constants.AuthBaseAddress}/login")
        {
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeout)
        };

        loginRequest.AddJsonBody(loginRequestDto.LoginDto);

        var loginResponse = await restClient.ExecutePostAsync<CustomResponse>(loginRequest);

        var getAllRequest = new RestRequest(Constants.UsersBaseAddress)
        {
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeout)
        };

        var getAllResponse = await restClient.ExecuteGetAsync<CustomResponse>(getAllRequest);

        var logoutRequest = new RestRequest($"{Constants.AuthBaseAddress}/logout")
        {
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeout)
        };

        var logoutResponse = await restClient.ExecutePostAsync<CustomResponse>(logoutRequest);

        switch (loginRequestDto.TestResult)
        {
            case AccountControllerTestResult.AllFailed:

                loginResponse.Data?.IsSuccess.Should().BeFalse();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

                logoutResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

                break;

            case AccountControllerTestResult.AllSuccessful:

                loginResponse.Data?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                break;

            case AccountControllerTestResult.Unauthorized:

                loginResponse.Data?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

                logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                break;

            default:
                Assert.True(false);
                break;
        }
    }

    public static TheoryData<LoginRequestDto> LoginTestData() =>
        new()
        {
            new LoginRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1376",
                    RememberMe = true
                },
                ShouldSucceed = true
            },
            new LoginRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "asdasdasdasd",
                    RememberMe = false
                },
                ShouldSucceed = false
            },
            new LoginRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_d",
                    Password = "sfr1376",
                    RememberMe = false
                },
                ShouldSucceed = false
            },
            new LoginRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "223",
                    Password = "sd2",
                    RememberMe = false
                },
                ShouldSucceed = false
            }
        };

    public static TheoryData<LoginGetAllLogoutRequestDto> LoginGetAllLogoutTestData() =>
        new()
        {
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "abbas_booazaar",
                    Password = "abbasabbas",
                    RememberMe = true
                },
                TestResult = AccountControllerTestResult.Unauthorized
            },
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1376",
                    RememberMe = true
                },
                TestResult = AccountControllerTestResult.AllSuccessful
            },
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1231123376",
                    RememberMe = true
                },
                TestResult = AccountControllerTestResult.AllFailed
            },
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepeasdfahr_frd",
                    Password = "sfr1376",
                    RememberMe = true
                },
                TestResult = AccountControllerTestResult.AllFailed
            }
        };

}