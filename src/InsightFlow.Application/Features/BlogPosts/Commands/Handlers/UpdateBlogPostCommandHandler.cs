using Common.Constants;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.BlogPosts.Commands.Handlers;

public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, DomainResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public UpdateBlogPostCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<DomainResponse> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.AuthorUuid,
            includes: [user => user.BlogPosts],
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return DomainResponse.CreateBaseFailure(
                StringConstants.Unauthenticated,
                StatusCodes.Status401Unauthorized);
        }

        var blogPost = await _unitOfWork.BlogPostRepository.GetOneAsync(
            blogPost => blogPost.Uuid == request.BlogPostUuid,
            cancellationToken: cancellationToken);

        if (blogPost is null)
        {
            var notFoundMessage = string.Format(StringConstants.EntityNotFoundByUuidTemplate, nameof(BlogPost), request.BlogPostUuid);

            return DomainResponse.CreateBaseFailure(notFoundMessage, StatusCodes.Status404NotFound);
        }

        _mappingService.Map(request, blogPost);

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            return DomainResponse.CreateBaseFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var blogPostResponseDto = _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost);

        if (blogPostResponseDto is null)
        {
            return DomainResponse.CreateBaseFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var message = string.Format(StringConstants.SuccessfulUpdateTemplate, nameof(BlogPost));

        return DomainResponse.CreateBaseSuccess(message, StatusCodes.Status200OK);
    }
}