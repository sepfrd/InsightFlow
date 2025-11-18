using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsCount;

public record GetAllBlogPostsCountQuery : IQuery<DomainResponse<long>>;