using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetSingleBlogPost;

public class GetSingleBlogPostQueryHandler : IRequestHandler<GetSingleBlogPostQuery, DomainResponse<BlogPostResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger<GetSingleBlogPostQueryHandler> _logger;

    public GetSingleBlogPostQueryHandler(
        IUnitOfWork unitOfWork,
        IMappingService mappingService,
        ILogger<GetSingleBlogPostQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<DomainResponse<BlogPostResponseDto>> Handle(GetSingleBlogPostQuery request, CancellationToken cancellationToken = default)
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

        if (blogPostResponseDto is not null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateSuccess(null, StatusCodes.Status200OK, blogPostResponseDto);
        }

        _logger.LogCritical(StringConstants.MappingErrorLogTemplate, typeof(BlogPost), typeof(BlogPostResponseDto));

        return DomainResponse<BlogPostResponseDto>.CreateFailure(
            StringConstants.InternalServerError,
            StatusCodes.Status500InternalServerError);
    }
}