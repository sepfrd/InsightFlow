using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsByFilter;

public record GetAllBlogPostsByFilterQuery(BlogPostFilterDto FilterDto, uint PageNumber, uint PageSize)
    : IRequest<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>;