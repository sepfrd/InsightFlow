using Common.Constants;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.BlogPosts.Queries.Handlers;

public class GetSingleBlogPostQueryHandler : IRequestHandler<GetSingleBlogPostQuery, DomainResponse<BlogPostResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public GetSingleBlogPostQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<DomainResponse<BlogPostResponseDto>> Handle(GetSingleBlogPostQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await _unitOfWork.BlogPostRepository.GetOneAsync(
            filter: blogPost => blogPost.Uuid == request.BlogPostUuid,
            includes: [blogPost => blogPost.Author],
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (blogPost is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(StringConstants.NotFound, StatusCodes.Status404NotFound);
        }

        var blogPostResponseDto = _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost);

        if (blogPostResponseDto is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        return DomainResponse<BlogPostResponseDto>.CreateSuccess(null, StatusCodes.Status200OK, blogPostResponseDto);
    }
}