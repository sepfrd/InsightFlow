using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsCount;

public class GetAllBlogPostsCountQueryHandler : IQueryHandler<GetAllBlogPostsCountQuery, DomainResponse<long>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBlogPostsCountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainResponse<long>> HandleAsync(GetAllBlogPostsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _unitOfWork.BlogPostRepository.GetCountAsync(null, cancellationToken);

        return DomainResponse<long>.CreateSuccess(null, StatusCodes.Status200OK, count);
    }
}