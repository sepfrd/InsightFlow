using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands;

public record DeleteBlogPostCommand(Guid BlogPostUuid, Guid AuthorUuid) : IRequest<DomainResponse>;