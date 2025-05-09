using Humanizer;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InsightFlow.Application.Features.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, DomainResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBlogPostCommandHandler> _logger;

    public DeleteBlogPostCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteBlogPostCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<DomainResponse> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.AuthorUuid,
            includes: [user => user.BlogPosts],
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return DomainResponse.CreateBaseFailure(StringConstants.Unauthorized, StatusCodes.Status401Unauthorized);
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

            return DomainResponse.CreateBaseFailure(notFoundMessage, StatusCodes.Status404NotFound);
        }

        if (blogPost.AuthorId != user.Id)
        {
            var forbiddenMessage = string.Format(
                StringConstants.ForbiddenActionTemplate,
                StringConstants.DeleteActionName.Humanize(LetterCasing.LowerCase),
                nameof(BlogPost).Humanize(LetterCasing.LowerCase));

            return DomainResponse.CreateBaseFailure(forbiddenMessage, StatusCodes.Status403Forbidden);
        }

        _unitOfWork.BlogPostRepository.Delete(blogPost);

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            _logger.LogCritical(StringConstants.DatabasePersistenceErrorLogTemplate, typeof(BlogPost), StringConstants.DeleteActionName);

            return DomainResponse.CreateBaseFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var message = string.Format(StringConstants.SuccessfulDeletionTemplate, nameof(BlogPost).Humanize(LetterCasing.LowerCase));

        return DomainResponse.CreateBaseSuccess(message, StatusCodes.Status200OK);
    }
}