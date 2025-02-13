using InsightFlow.Application.Extensions;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Queries.Handlers;

public class GetUserBlogPostsQueryHandler : IRequestHandler<GetUserBlogPostsQuery, PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public GetUserBlogPostsQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>> Handle(GetUserBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.UserUuid,
            includes: [user => user.BlogPosts],
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>.CreateFailure(DomainErrors.Unauthenticated, DomainErrors.Unauthenticated.Description);
        }

        var totalCount = user.BlogPosts.Count;

        var blogPosts = user.BlogPosts
                                .OrderBy(blogPost => blogPost.Id)
                                .Paginate(request.PageNumber, request.PageSize);

        var blogPostDtos = _mappingService.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostResponseDto>>(blogPosts);

        var response = new PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>(request.PageNumber, request.PageSize, totalCount, blogPostDtos);

        return response;
    }
}