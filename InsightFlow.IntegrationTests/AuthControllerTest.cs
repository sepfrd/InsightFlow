using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.IntegrationTests.Common;
using InsightFlow.IntegrationTests.Common.Dtos;
using InsightFlow.IntegrationTests.Common.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;
using RestSharp.Authenticators;
using Xunit;

namespace InsightFlow.IntegrationTests;

public class AuthControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const int DefaultTimeout = 2000;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AuthControllerTest(CustomWebApplicationFactory<Program> factory)
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
    [MemberData(nameof(LoginGetAllTestData))]
    public async Task LoginGetAll_ReturnExpectedResult(LoginGetAllLogoutRequestDto loginRequestDto)
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

        var loginResponse = await restClient.ExecutePostAsync<CustomResponse<string>>(loginRequest);

        var getAllRequest = new RestRequest(Constants.AdminUsersBaseAddress)
        {
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeout)
        };

        if (loginResponse.IsSuccessful)
        {
            var authToken = loginResponse.Data?.Data!;

            getAllRequest.Authenticator = new JwtAuthenticator(authToken);
        }

        var getAllResponse = await restClient.ExecuteGetAsync<CustomResponse>(getAllRequest);

        switch (loginRequestDto.TestResult)
        {
            case AuthControllerTestResult.AllFailed:

                loginResponse.Data?.IsSuccess.Should().BeFalse();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

                break;

            case AuthControllerTestResult.AllSuccessful:

                loginResponse.Data?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                break;

            case AuthControllerTestResult.Forbidden:

                loginResponse.Data?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

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
                    Password = "Sfr1376.",
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
                    Password = "Sfr1376.",
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

    public static TheoryData<LoginGetAllLogoutRequestDto> LoginGetAllTestData() =>
        new()
        {
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "bernard_cool",
                    Password = "BernardCool1997.",
                    RememberMe = true
                },
                TestResult = AuthControllerTestResult.Forbidden
            },
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "Sfr1376.",
                    RememberMe = true
                },
                TestResult = AuthControllerTestResult.AllSuccessful
            },
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1231123376",
                    RememberMe = true
                },
                TestResult = AuthControllerTestResult.AllFailed
            },
            new LoginGetAllLogoutRequestDto
            {
                LoginDto = new LoginDto
                {
                    Username = "sepeasdfahr_frd",
                    Password = "Sfr1376.",
                    RememberMe = true
                },
                TestResult = AuthControllerTestResult.AllFailed
            }
        };

}