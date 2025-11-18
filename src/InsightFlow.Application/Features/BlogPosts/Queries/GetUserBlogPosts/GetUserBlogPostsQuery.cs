using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetUserBlogPosts;

public record GetUserBlogPostsQuery(Guid UserUuid, uint PageNumber = 1, uint PageSize = 10)
    : IQuery<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>;