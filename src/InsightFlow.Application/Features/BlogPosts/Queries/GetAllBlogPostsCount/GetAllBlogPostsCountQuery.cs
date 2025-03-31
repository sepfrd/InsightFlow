using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsCount;

public record GetAllBlogPostsCountQuery : IRequest<DomainResponse<long>>;