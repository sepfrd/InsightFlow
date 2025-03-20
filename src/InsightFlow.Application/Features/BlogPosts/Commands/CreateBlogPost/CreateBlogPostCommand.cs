using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;

public record CreateBlogPostCommand(string Title, string Body, Guid AuthorUuid) : IRequest<DomainResponse<BlogPostResponseDto>>;