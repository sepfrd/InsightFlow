using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.UnitTests.Features.TestUtilities.BlogPosts.Extensions;

public static class BlogPostExtensions
{
    public static BlogPostResponseDto ToBlogPostResponseDto(this BlogPost blogPost, User? author = null)
    {
        author ??= blogPost.Author!;

        return new BlogPostResponseDto(
            blogPost.Uuid,
            blogPost.CreatedAt,
            blogPost.UpdatedAt,
            blogPost.Title,
            blogPost.Body,
            new UserResponseDto(
                author.Uuid,
                author.Username,
                author.Email,
                author.FirstName + ' ' + author.LastName));
    }
}