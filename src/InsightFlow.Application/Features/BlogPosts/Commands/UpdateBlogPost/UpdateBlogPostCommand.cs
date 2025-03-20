using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands.UpdateBlogPost;

public record UpdateBlogPostCommand(Guid AuthorUuid, Guid BlogPostUuid, string NewTitle, string NewBody)
    : IRequest<DomainResponse<BlogPostResponseDto>>;