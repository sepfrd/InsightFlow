using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands;

public record UpdateBlogPostCommand(Guid BlogPostUuid, string NewTitle, string NewDescription, Guid AuthorUuid)
    : IRequest<DomainResponse>;