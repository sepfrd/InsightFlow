using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetUserBlogPosts;

public class GetUserBlogPostsQueryHandler : IRequestHandler<GetUserBlogPostsQuery, PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger<GetUserBlogPostsQueryHandler> _logger;

    public GetUserBlogPostsQueryHandler(
        IUnitOfWork unitOfWork,
        IMappingService mappingService,
        ILogger<GetUserBlogPostsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>> Handle(GetUserBlogPostsQuery request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.UserUuid,
            includes: [user => user.BlogPosts],
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>.CreateFailure(
                StringConstants.Unauthorized,
                StatusCodes.Status401Unauthorized);
        }

        var blogPostsResponse = await _unitOfWork.BlogPostRepository.GetAllAsync(
            filter: blogPost => blogPost.AuthorId == user.Id,
            includes: [blogPost => blogPost.Author],
            page: request.PageNumber,
            limit: request.PageSize,
            disableTracking: true,
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