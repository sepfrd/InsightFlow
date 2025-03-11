using Common.Constants;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.BlogPosts.Queries.Handlers;

public class GetAllBlogPostsByFilterQueryHandler
    : IRequestHandler<GetAllBlogPostsByFilterQuery, PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public GetAllBlogPostsByFilterQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>> Handle(GetAllBlogPostsByFilterQuery request, CancellationToken cancellationToken)
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