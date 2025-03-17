using Humanizer;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InsightFlow.Application.Features.BlogPosts.Commands.Handlers;

public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, DomainResponse<BlogPostResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger<UpdateBlogPostCommandHandler> _logger;

    public UpdateBlogPostCommandHandler(
        IUnitOfWork unitOfWork,
        IMappingService mappingService,
        ILogger<UpdateBlogPostCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<DomainResponse<BlogPostResponseDto>> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.AuthorUuid,
            includes: [user => user.BlogPosts],
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.Unauthenticated,
                StatusCodes.Status401Unauthorized);
        }

        var blogPost = await _unitOfWork.BlogPostRepository.GetOneAsync(
            blogPost => blogPost.Uuid == request.BlogPostUuid,
            cancellationToken: cancellationToken);

        if (blogPost is null)
        {
            var notFoundMessage = string.Format(
                StringConstants.EntityNotFoundByUuidTemplate,
                nameof(BlogPost).Humanize(LetterCasing.LowerCase),
                request.BlogPostUuid);

            return DomainResponse<BlogPostResponseDto>.CreateFailure(notFoundMessage, StatusCodes.Status404NotFound);
        }

        if (blogPost.AuthorId != user.Id)
        {
            var forbiddenMessage = string.Format(
                StringConstants.ForbiddenActionTemplate,
                StringConstants.UpdateActionName,
                nameof(BlogPost).Humanize(LetterCasing.LowerCase));

            return DomainResponse<BlogPostResponseDto>.CreateFailure(forbiddenMessage, StatusCodes.Status403Forbidden);
        }

        if (request.NewTitle == blogPost.Title && request.NewBody == blogPost.Body)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.IdenticalNewPropertyValuesTemplate,
                StatusCodes.Status400BadRequest);
        }

        var updatedBlogPost = _mappingService.Map(request, blogPost);

        if (updatedBlogPost is null)
        {
            _logger.LogCritical(
                StringConstants.MappingErrorLogTemplate,
                typeof(UpdateBlogPostCommand),
                typeof(BlogPost));

            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        updatedBlogPost.PrepareForUpdate();

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            _logger.LogCritical(StringConstants.DatabasePersistenceErrorLogTemplate, typeof(BlogPost), StringConstants.UpdateActionName);

            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var blogPostResponseDto = _mappingService.Map<BlogPost, BlogPostResponseDto>(updatedBlogPost);

        if (blogPostResponseDto is null)
        {
            _logger.LogCritical(StringConstants.MappingErrorLogTemplate, typeof(BlogPost), typeof(BlogPostResponseDto));

            return DomainResponse<BlogPostResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var message = string.Format(StringConstants.SuccessfulUpdateTemplate, nameof(BlogPost).Humanize(LetterCasing.LowerCase));

        return DomainResponse<BlogPostResponseDto>.CreateSuccess(message, StatusCodes.Status200OK, blogPostResponseDto);
    }
}