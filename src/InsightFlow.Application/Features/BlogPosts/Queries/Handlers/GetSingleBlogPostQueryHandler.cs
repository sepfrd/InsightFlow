using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

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
            blogPost => blogPost.Uuid == request.BlogPostUuid,
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (blogPost is null)
        {
            return new DomainResponse<BlogPostResponseDto>(null, DomainErrors.Unauthenticated, DomainErrors.Unauthenticated.Description);
        }

        var blogPostResponseDto = _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost);

        return new DomainResponse<BlogPostResponseDto>(blogPostResponseDto);
    }
}