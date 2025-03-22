namespace InsightFlow.Application.UnitTests.Features.TestUtilities.Constants;

public static partial class Constants
{
    public static class BlogPost
    {
        public const long Id = 1L;
        public const string Title = "Blog Post Title";
        public const string Body = "Blog Post Body";

        public static Domain.Entities.BlogPost CreateBlogPost(
            string? title = null,
            string? body = null,
            Domain.Entities.User? author = null)
        {
            author ??= User.CreateUser();

            return new Domain.Entities.BlogPost
            {
                Id = Id,
                Title = title ?? Title,
                Body = body ?? Body,
                AuthorId = author.Id,
                Author = author
            };
        }
    }
}