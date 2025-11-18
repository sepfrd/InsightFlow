using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsByFilter;

public record GetAllBlogPostsByFilterQuery(BlogPostFilterDto FilterDto, uint PageNumber, uint PageSize)
    : IQuery<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>;