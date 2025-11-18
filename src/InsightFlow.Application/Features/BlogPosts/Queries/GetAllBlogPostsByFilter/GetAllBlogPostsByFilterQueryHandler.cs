using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsByFilter;

public class GetAllBlogPostsByFilterQueryHandler
    : IQueryHandler<GetAllBlogPostsByFilterQuery, PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger<GetAllBlogPostsByFilterQueryHandler> _logger;

    public GetAllBlogPostsByFilterQueryHandler(
        IUnitOfWork unitOfWork,
        IMappingService mappingService,
        ILogger<GetAllBlogPostsByFilterQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>> HandleAsync(GetAllBlogPostsByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterExpression = request.FilterDto.ToExpression() ?? (_ => true);

        var blogPostsResponse = await _unitOfWork.BlogPostRepository.GetAllAsync(
            filter: filterExpression,
            includes: [blogPost => blogPost.Author],
            page: request.PageNumber,
            limit: request.PageSize,
            cancellationToken: cancellationToken);

        var blogPostDtos = _mappingService.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostResponseDto>>(blogPostsResponse.Data);

        if (blogPostDtos is null)
        {
            _logger.LogCritical(StringConstants.MappingErrorLogTemplate, typeof(IEnumerable<BlogPost>), typeof(IEnumerable<BlogPostResponseDto>));

            return PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var response = PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>.CreateSuccess(
            null,
            StatusCodes.Status200OK,
            blogPostDtos,
            request.PageNumber,
            request.PageSize,
            blogPostsResponse.TotalCount);

        return response;
    }
}