using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands.DeleteBlogPost;

public record DeleteBlogPostCommand(Guid BlogPostUuid, Guid AuthorUuid) : IRequest<DomainResponse>;