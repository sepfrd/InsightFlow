using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetSingleBlogPost;

public record GetSingleBlogPostQuery(Guid BlogPostUuid) : IQuery<DomainResponse<BlogPostResponseDto>>;