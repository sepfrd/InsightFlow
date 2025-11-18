using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Commands.DeleteBlogPost;

public record DeleteBlogPostCommand(Guid BlogPostUuid, Guid AuthorUuid) : ICommand<DomainResponse>;