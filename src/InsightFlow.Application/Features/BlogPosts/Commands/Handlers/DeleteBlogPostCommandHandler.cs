using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands.Handlers;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, DomainResponse<BlogPostResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public DeleteBlogPostCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<DomainResponse<BlogPostResponseDto>> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.AuthorUuid,
            includes: [user => user.BlogPosts],
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(DomainErrors.Unauthenticated, DomainErrors.Unauthenticated.Description);
        }

        var blogPost = await _unitOfWork.BlogPostRepository.GetOneAsync(
                    blogPost => blogPost.Uuid == request.BlogPostUuid,
                    cancellationToken: cancellationToken);

        if (blogPost is null)
        {
            return DomainResponse<BlogPostResponseDto>.CreateFailure(DomainErrors.NotFound, DomainErrors.NotFound.Description);
        }

        _unitOfWork.BlogPostRepository.Delete(blogPost);

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            return new DomainResponse<BlogPostResponseDto>(null, DomainErrors.InternalServerError, DomainErrors.InternalServerError.Description);
        }

        var blogPostResponseDto = _mappingService.Map<BlogPost, BlogPostResponseDto>(blogPost);

        return new DomainResponse<BlogPostResponseDto>(blogPostResponseDto);
    }
}