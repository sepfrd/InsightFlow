using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.UnitTests.Features.TestUtilities.BlogPosts.Extensions;

public static class BlogPostExtensions
{
    public static BlogPostResponseDto ToBlogPostResponseDto(this BlogPost blogPost, Guid authorUuid) =>
        new(blogPost.Uuid, blogPost.Title, blogPost.Body, authorUuid);
}