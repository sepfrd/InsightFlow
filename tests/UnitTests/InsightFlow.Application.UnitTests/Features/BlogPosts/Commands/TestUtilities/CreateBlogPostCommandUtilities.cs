using InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;
using InsightFlow.Application.UnitTests.Features.TestUtilities.Constants;
using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.UnitTests.Features.BlogPosts.Commands.TestUtilities;

public class CreateBlogPostCommandUtilities
{
    public static CreateBlogPostCommand CreateCommand(
        BlogPost? blogPost = null,
        User? user = null) =>
        new(
            blogPost?.Title ?? Constants.BlogPost.Title,
            blogPost?.Body ?? Constants.BlogPost.Body,
            user?.Uuid ?? Constants.User.Uuid);
}