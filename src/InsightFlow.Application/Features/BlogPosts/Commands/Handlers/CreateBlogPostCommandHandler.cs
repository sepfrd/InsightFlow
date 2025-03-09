using Common.Constants;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.BlogPosts.Commands.Handlers;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, DomainResponse<BlogPostResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public CreateBlogPostCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<DomainResponse<BlogPostResponseDto>> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.AuthorUuid,
            includes: [user => user.BlogPosts],
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.Unauthenticated,
                StatusCodes.Status401Unauthorized);
        }

        var blogPost = _mappingService.Map<CreateBlogPostCommand, BlogPost>(request)!;

        await _unitOfWork.BlogPostRepository.CreateAsync(blogPost, cancellationToken);

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var blogPostResponseDto = _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost);

        if (blogPostResponseDto is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var message = string.Format(StringConstants.SuccessfulCreationTemplate, nameof(BlogPost));

        return DomainResponse<BlogPostResponseDto>.CreateSuccess(message, StatusCodes.Status201Created, blogPostResponseDto);
    }
}