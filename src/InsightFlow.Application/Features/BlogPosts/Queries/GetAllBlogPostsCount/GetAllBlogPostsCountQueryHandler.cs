using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsCount;

public class GetAllBlogPostsCountQueryHandler : IRequestHandler<GetAllBlogPostsCountQuery, DomainResponse<long>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBlogPostsCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainResponse<long>> Handle(GetAllBlogPostsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.BlogPostRepository.GetCountAsync(null, cancellationToken);

        return DomainResponse<long>.CreateSuccess(null, StatusCodes.Status200OK, count);
    }
}