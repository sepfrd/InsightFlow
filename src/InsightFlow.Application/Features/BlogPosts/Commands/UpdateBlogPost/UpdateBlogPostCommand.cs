using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Commands.UpdateBlogPost;

public record UpdateBlogPostCommand(Guid AuthorUuid, Guid BlogPostUuid, string NewTitle, string NewBody)
    : ICommand<DomainResponse<BlogPostResponseDto>>;