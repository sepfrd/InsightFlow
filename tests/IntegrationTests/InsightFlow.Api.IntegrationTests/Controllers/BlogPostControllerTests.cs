using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Humanizer;
using InsightFlow.Api.Common.Dtos.Requests;
using InsightFlow.Api.IntegrationTests.TestUtilities;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shouldly;

namespace InsightFlow.Api.IntegrationTests.Controllers;

[Collection(Constants.DefaultTestCollectionName)]
public class BlogPostsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public BlogPostsControllerTests(CustomWebApplicationFactory<Program> customWebApplicationFactory)
    {
        _customWebApplicationFactory = customWebApplicationFactory;
    }

    [Theory]
    [MemberData(nameof(ValidAuthenticateAndThenCreateBlogPostRequests))]
    public async Task AuthenticateAndThenCreateBlogPost_WhenRequestIsValid_ShouldCreateAndReturnCreatedResponse(
        LoginDto loginDto,
        CreateBlogPostRequestDto createBlogPostDto)
    {
        // Authenticate --- Arrange
        var httpClient = _customWebApplicationFactory.CreateClient();
        var loginRequest = new HttpRequestMessage(HttpMethod.Post, Constants.LoginAddress);
        loginRequest.Content = JsonContent.Create(loginDto, options: _jsonSerializerOptions);

        // Authenticate --- Act
        var authResponse = await httpClient.SendAsync(
            loginRequest,
            TestContext.Current.CancellationToken);

        var authResponseString = await authResponse.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var authResponseJson = JsonNode.Parse(authResponseString)!;

        var jwt = authResponseJson[nameof(DomainResponse<>.Data).Camelize()]?.ToString();
        var userUuid = GetUserUuidFromJwt(jwt);
        var authResponseMessage = authResponseJson[nameof(DomainResponse<>.Message).Camelize()]?.ToString();
        var authResponseIsSuccess = authResponseJson[nameof(DomainResponse<>.IsSuccess).Camelize()];

        // Authenticate --- Assert
        authResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        authResponseIsSuccess.ShouldNotBeNull();
        ((bool)authResponseIsSuccess).ShouldBe(true);

        jwt.ShouldNotBeNull();

        userUuid.ShouldNotBeNull();
        userUuid.ShouldBeOfType<Guid>();

        authResponseMessage.ShouldNotBeNull();
        authResponseMessage.ShouldBe(StringConstants.SuccessfulLogin);

        // Create a BlogPost --- Arrange

        var createBlogPostRequest = new HttpRequestMessage(HttpMethod.Post, Constants.BlogPostsBaseAddress);
        createBlogPostRequest.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwt);
        createBlogPostRequest.Content = JsonContent.Create(createBlogPostDto, options: _jsonSerializerOptions);

        var createdAtDateTime = DateTime.UtcNow;

        // Create a BlogPost --- Act
        var createBlogPostResponse = await httpClient.SendAsync(
            createBlogPostRequest,
            TestContext.Current.CancellationToken);

        var createBlogPostResponseString = await createBlogPostResponse.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var createBlogPostResponseJson = JsonNode.Parse(createBlogPostResponseString)!;

        var blogPostJson = createBlogPostResponseJson[nameof(DomainResponse<>.Data).Camelize()];
        var createBlogPostResponseMessage = createBlogPostResponseJson[nameof(DomainResponse<>.Message).Camelize()]?.ToString();
        var createBlogPostResponseIsSuccess = createBlogPostResponseJson[nameof(DomainResponse<>.IsSuccess).Camelize()];

        // Create a BlogPost --- Assert
        using var scope = _customWebApplicationFactory.Services.CreateScope();
        var injectedUnitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        injectedUnitOfWork.ShouldBeAssignableTo<UnitOfWork>();

        var unitOfWork = (UnitOfWork)injectedUnitOfWork;

        var createBlogPostResponseExpectedMessage = string.Format(
            StringConstants.SuccessfulCreationTemplate,
            nameof(BlogPost).Humanize(LetterCasing.LowerCase));

        createBlogPostResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
        createBlogPostResponseIsSuccess.ShouldNotBeNull();
        ((bool)createBlogPostResponseIsSuccess).ShouldBe(true);

        blogPostJson.ShouldNotBeNull();

        var blogPostTitle = blogPostJson[nameof(BlogPostResponseDto.Title).Camelize()]?.ToString();
        var blogPostBody = blogPostJson[nameof(BlogPostResponseDto.Body).Camelize()]?.ToString();
        var blogPostAuthorJson = blogPostJson[nameof(BlogPostResponseDto.Author).Camelize()];

        blogPostAuthorJson.ShouldNotBeNull();

        var authorUuidString = blogPostAuthorJson[nameof(UserResponseDto.Uuid).Camelize()]?.ToString();
        var authorUsernameString = blogPostAuthorJson[nameof(UserResponseDto.Username).Camelize()]?.ToString();
        var authorEmailString = blogPostAuthorJson[nameof(UserResponseDto.Email).Camelize()]?.ToString();
        var authorFullNameString = blogPostAuthorJson[nameof(UserResponseDto.FullName).Camelize()]?.ToString();

        blogPostTitle.ShouldNotBeNull();
        blogPostTitle.ShouldBe(createBlogPostDto.Title);

        blogPostBody.ShouldNotBeNull();
        blogPostBody.ShouldBe(createBlogPostDto.Body);

        authorUuidString.ShouldNotBeNull().ShouldBe(userUuid.ToString());

        loginDto.UsernameOrEmail.ShouldNotBeNull().ShouldBeOneOf(authorUsernameString, authorEmailString);

        createBlogPostResponseMessage.ShouldNotBeNull();
        createBlogPostResponseMessage.ShouldBe(createBlogPostResponseExpectedMessage);

        var createdBlogPost = await unitOfWork
            .BlogPosts
            .OrderByDescending(blogPost => blogPost.CreatedAt)
            .FirstAsync(cancellationToken: TestContext.Current.CancellationToken);

        var user = await unitOfWork
            .Users
            .FirstAsync(
                user => user.Uuid == userUuid,
                cancellationToken: TestContext.Current.CancellationToken);

        createdBlogPost.Title.ShouldBe(createBlogPostDto.Title);
        createdBlogPost.CreatedAt.ShouldBeGreaterThanOrEqualTo(createdAtDateTime);
        createdBlogPost.UpdatedAt.ShouldBeGreaterThanOrEqualTo(createdAtDateTime);
        createdBlogPost.CreatedAt.ShouldBe(createdBlogPost.UpdatedAt);
        createdBlogPost.Title.ShouldBe(createBlogPostDto.Title);
        createdBlogPost.Body.ShouldBe(createBlogPostDto.Body);
        createdBlogPost.AuthorId.ShouldBe(user.Id);

        authorUuidString.ShouldBe(user.Uuid.ToString());
        authorUsernameString.ShouldBe(user.Username);
        authorEmailString.ShouldBe(user.Email);
        authorFullNameString.ShouldBe(user.FirstName + ' ' + user.LastName);
    }

    public static TheoryData<LoginDto, CreateBlogPostRequestDto> ValidAuthenticateAndThenCreateBlogPostRequests() =>
        new()
        {
            {
                new LoginDto("sepehr_frd", "Sfr1376."),
                new CreateBlogPostRequestDto("Some Title", "This is a test body for the blog post.")
            },
            {
                new LoginDto("sepfrd@outlook.com", "Sfr1376."),
                new CreateBlogPostRequestDto("Some Other Title", "This is another test body for the blog post.")
            }
        };

    private static Guid? GetUserUuidFromJwt(string? jwt)
    {
        if (jwt is null)
        {
            return null;
        }

        var tokenSections = jwt.Split('.');

        var tokenPayloadString = tokenSections[1];

        var decodedPayloadBytes = Base64UrlEncoder.DecodeBytes(tokenPayloadString);

        var decodedPayloadString = Encoding.UTF8.GetString(decodedPayloadBytes);

        var decodedPayloadJson = JsonNode.Parse(decodedPayloadString);

        if (decodedPayloadJson is null)
        {
            return null;
        }

        var userUuidString = decodedPayloadJson[InfrastructureConstants.UuidClaim]?.ToString();

        var isGuid = Guid.TryParse(userUuidString, out var userUuid);

        return isGuid ? userUuid : null;
    }
}