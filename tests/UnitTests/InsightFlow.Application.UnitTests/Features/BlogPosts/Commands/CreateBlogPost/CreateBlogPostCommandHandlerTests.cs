using Humanizer;
using InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Application.UnitTests.Features.BlogPosts.Commands.TestUtilities;
using InsightFlow.Application.UnitTests.Features.TestUtilities.BlogPosts.Extensions;
using InsightFlow.Application.UnitTests.Features.TestUtilities.Constants;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;

namespace InsightFlow.Application.UnitTests.Features.BlogPosts.Commands.CreateBlogPost;

public class CreateBlogPostCommandHandlerTests
{
    private readonly CreateBlogPostCommandHandler _commandHandler;
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMappingService _mappingService = Substitute.For<IMappingService>();
    private readonly ILogger<CreateBlogPostCommandHandler> _logger = Substitute.For<ILogger<CreateBlogPostCommandHandler>>();

    public CreateBlogPostCommandHandlerTests()
    {
        _commandHandler = new CreateBlogPostCommandHandler(_unitOfWork, _mappingService, _logger);
    }

    [Theory]
    [MemberData(nameof(ValidCreateBlogPostCommands))]
    public async Task HandleCreateBlogPostCommand_WhenBlogPostIsValid_ShouldCreateAndReturnBlogPost(CreateBlogPostCommand createBlogPostCommand)
    {
        // Arrange
        var userRole = Constants.UserRole.CreateUserRole();
        var user = Constants.User.CreateUser(uuid: createBlogPostCommand.AuthorUuid, userRoles: [userRole]);

        userRole.UserId = user.Id;

        var blogPost = Constants.BlogPost.CreateBlogPost(createBlogPostCommand.Title, createBlogPostCommand.Body, user);

        var blogPostResponseDto = blogPost.ToBlogPostResponseDto();

        _unitOfWork
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken)
            .ReturnsForAnyArgs(user);

        _unitOfWork.CommitChangesAsync(TestContext.Current.CancellationToken).ReturnsForAnyArgs(1);

        _mappingService.Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand).Returns(blogPost);
        _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost).Returns(blogPostResponseDto);

        _unitOfWork.ClearReceivedCalls();
        _mappingService.ClearReceivedCalls();

        // Act
        var result = await _commandHandler.Handle(createBlogPostCommand, TestContext.Current.CancellationToken);

        // Assert
        var responseMessage = string.Format(StringConstants.SuccessfulCreationTemplate, nameof(BlogPost).Humanize(LetterCasing.LowerCase));

        result.ShouldBeOfType<DomainResponse<BlogPostResponseDto>>();
        result.IsSuccess.ShouldBeTrue();
        result.StatusCode.ShouldBe(StatusCodes.Status201Created);
        result.Message.ShouldBe(responseMessage);
        result.Data.ValidateCreatedFrom(createBlogPostCommand);

        await _unitOfWork
            .ReceivedWithAnyArgs(1)
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken);

        await _unitOfWork
            .Received(1)
            .BlogPostRepository
            .CreateAsync(blogPost, TestContext.Current.CancellationToken);

        await _unitOfWork
            .ReceivedWithAnyArgs(1)
            .CommitChangesAsync(TestContext.Current.CancellationToken);

        _mappingService.Received(1).Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand);
        _mappingService.Received(1).Map<BlogPost, BlogPostResponseDto>(blogPost);

        _logger.DidNotReceive();
    }

    [Theory]
    [MemberData(nameof(ValidCreateBlogPostCommands))]
    public async Task HandleCreateBlogPostCommand_WhenAuthorIsNotFound_ShouldReturnUnauthorizedResponse(CreateBlogPostCommand createBlogPostCommand)
    {
        // Arrange
        var userRole = Constants.UserRole.CreateUserRole();
        var user = Constants.User.CreateUser(uuid: createBlogPostCommand.AuthorUuid, userRoles: [userRole]);

        userRole.UserId = user.Id;

        var blogPost = Constants.BlogPost.CreateBlogPost(createBlogPostCommand.Title, createBlogPostCommand.Body, user);

        var blogPostResponseDto = blogPost.ToBlogPostResponseDto();

        _unitOfWork
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken)
            .ReturnsNullForAnyArgs();

        _unitOfWork.CommitChangesAsync(TestContext.Current.CancellationToken).ReturnsForAnyArgs(1);

        _mappingService.Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand).Returns(blogPost);
        _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost).Returns(blogPostResponseDto);

        _unitOfWork.ClearReceivedCalls();
        _mappingService.ClearReceivedCalls();

        // Act
        var result = await _commandHandler.Handle(createBlogPostCommand, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeOfType<DomainResponse<BlogPostResponseDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.StatusCode.ShouldBe(StatusCodes.Status401Unauthorized);
        result.Message.ShouldBe(StringConstants.Unauthorized);
        result.Data.ShouldBeNull();

        await _unitOfWork
            .ReceivedWithAnyArgs(1)
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken);

        await _unitOfWork
            .DidNotReceive()
            .BlogPostRepository
            .CreateAsync(blogPost, TestContext.Current.CancellationToken);

        await _unitOfWork
            .DidNotReceive()
            .CommitChangesAsync(TestContext.Current.CancellationToken);

        _mappingService.DidNotReceive().Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand);
        _mappingService.DidNotReceive().Map<BlogPost, BlogPostResponseDto>(blogPost);

        _logger.DidNotReceive();
    }

    [Theory]
    [MemberData(nameof(ValidCreateBlogPostCommands))]
    public async Task HandleCreateBlogPostCommand_WhenMappingDoesNotWorkCorrectly_ShouldReturnInternalServerErrorResponse(CreateBlogPostCommand createBlogPostCommand)
    {
        // Arrange
        var userRole = Constants.UserRole.CreateUserRole();
        var user = Constants.User.CreateUser(uuid: createBlogPostCommand.AuthorUuid, userRoles: [userRole]);

        userRole.UserId = user.Id;

        var blogPost = Constants.BlogPost.CreateBlogPost(createBlogPostCommand.Title, createBlogPostCommand.Body, user);

        _unitOfWork
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken)
            .ReturnsForAnyArgs(user);

        _unitOfWork.CommitChangesAsync(TestContext.Current.CancellationToken).ReturnsForAnyArgs(1);

        _mappingService.Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand).ReturnsNullForAnyArgs();
        _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost).ReturnsNullForAnyArgs();

        _unitOfWork.ClearReceivedCalls();
        _mappingService.ClearReceivedCalls();

        // Act
        var result = await _commandHandler.Handle(createBlogPostCommand, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeOfType<DomainResponse<BlogPostResponseDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        result.Message.ShouldBe(StringConstants.InternalServerError);
        result.Data.ShouldBeNull();

        await _unitOfWork
            .ReceivedWithAnyArgs(1)
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken);

        await _unitOfWork
            .DidNotReceive()
            .BlogPostRepository
            .CreateAsync(blogPost, TestContext.Current.CancellationToken);

        await _unitOfWork
            .DidNotReceive()
            .CommitChangesAsync(TestContext.Current.CancellationToken);

        _mappingService.Received(1).Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand);
        _mappingService.DidNotReceive().Map<BlogPost, BlogPostResponseDto>(blogPost);

        _logger.ReceivedWithAnyArgs(1).LogCritical("");
    }

    [Theory]
    [MemberData(nameof(ValidCreateBlogPostCommands))]
    public async Task HandleCreateBlogPostCommand_WhenUnitOfWorkDoesNotWorkCorrectly_ShouldReturnInternalServerErrorResponse(CreateBlogPostCommand createBlogPostCommand)
    {
        // Arrange
        var userRole = Constants.UserRole.CreateUserRole();
        var user = Constants.User.CreateUser(uuid: createBlogPostCommand.AuthorUuid, userRoles: [userRole]);

        userRole.UserId = user.Id;

        var blogPost = Constants.BlogPost.CreateBlogPost(createBlogPostCommand.Title, createBlogPostCommand.Body, user);

        var blogPostResponseDto = blogPost.ToBlogPostResponseDto();

        _unitOfWork
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken)
            .ReturnsForAnyArgs(user);

        _unitOfWork
            .CommitChangesAsync(TestContext.Current.CancellationToken)
            .ReturnsForAnyArgs(0);

        _mappingService.Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand).Returns(blogPost);
        _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost).Returns(blogPostResponseDto);

        _unitOfWork.ClearReceivedCalls();
        _mappingService.ClearReceivedCalls();

        // Act
        var result = await _commandHandler.Handle(createBlogPostCommand, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeOfType<DomainResponse<BlogPostResponseDto>>();
        result.IsSuccess.ShouldBeFalse();
        result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        result.Message.ShouldBe(StringConstants.InternalServerError);
        result.Data.ShouldBeNull();

        await _unitOfWork
            .ReceivedWithAnyArgs(1)
            .UserRepository
            .GetOneAsync(null!, cancellationToken: TestContext.Current.CancellationToken);

        await _unitOfWork
            .Received(1)
            .BlogPostRepository
            .CreateAsync(blogPost, TestContext.Current.CancellationToken);

        await _unitOfWork
            .Received(1)
            .CommitChangesAsync(TestContext.Current.CancellationToken);

        _mappingService.Received(1).Map<CreateBlogPostCommand, BlogPost>(createBlogPostCommand);
        _mappingService.DidNotReceive().Map<BlogPost, BlogPostResponseDto>(blogPost);

        _logger.ReceivedWithAnyArgs(1).LogCritical("");
    }

    public static TheoryData<CreateBlogPostCommand> ValidCreateBlogPostCommands() =>
        new()
        {
            CreateBlogPostCommandUtilities.CreateCommand(),
            CreateBlogPostCommandUtilities.CreateCommand(
                Constants.BlogPost.CreateBlogPost(Constants.BlogPost.Title + " and Something Else")),
            CreateBlogPostCommandUtilities.CreateCommand(
                Constants.BlogPost.CreateBlogPost(body: Constants.BlogPost.Body + " and something else")),
            CreateBlogPostCommandUtilities.CreateCommand(
                Constants.BlogPost.CreateBlogPost(
                    Constants.BlogPost.Title + " and Something Else",
                    Constants.BlogPost.Body + " and something else")),
            CreateBlogPostCommandUtilities.CreateCommand(
                Constants.BlogPost.CreateBlogPost(author: Constants.User.CreateUser(Guid.CreateVersion7()))),
            CreateBlogPostCommandUtilities.CreateCommand(
                Constants.BlogPost.CreateBlogPost(
                    Constants.BlogPost.Title + " and Something Else",
                    Constants.BlogPost.Body + " and something else",
                    Constants.User.CreateUser(Guid.CreateVersion7())))
        };
}