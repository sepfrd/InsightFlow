using InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using Shouldly;

namespace InsightFlow.Application.UnitTests.Features.TestUtilities.BlogPosts.Extensions;

public static class BlogPostResponseDtoExtensions
{
    public static void ValidateCreatedFrom(this BlogPostResponseDto? blogPostResponseDto, CreateBlogPostCommand createBlogPostCommand)
    {
        blogPostResponseDto.ShouldNotBeNull();
        blogPostResponseDto.Title.ShouldBe(createBlogPostCommand.Title);
        blogPostResponseDto.Body.ShouldBe(createBlogPostCommand.Body);
        blogPostResponseDto.AuthorUuid.ShouldBe(createBlogPostCommand.AuthorUuid);
    }
}