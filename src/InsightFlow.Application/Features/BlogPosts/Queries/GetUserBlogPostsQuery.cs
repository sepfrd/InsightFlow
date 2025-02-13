using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Queries;

public record GetUserBlogPostsQuery(Guid UserUuid, int PageNumber = 1, int PageSize = 10) :
    IRequest<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>;