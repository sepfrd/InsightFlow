using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetUserBlogPosts;

public record GetUserBlogPostsQuery(Guid UserUuid, uint PageNumber = 1, uint PageSize = 10)
    : IRequest<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>;