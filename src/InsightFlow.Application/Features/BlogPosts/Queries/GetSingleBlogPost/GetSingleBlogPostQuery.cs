using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Queries.GetSingleBlogPost;

public record GetSingleBlogPostQuery(Guid BlogPostUuid) : IRequest<DomainResponse<BlogPostResponseDto>>;