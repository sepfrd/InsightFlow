using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;

public record CreateBlogPostCommand(string Title, string Body, Guid AuthorUuid)
    : ICommand<DomainResponse<BlogPostResponseDto>>;