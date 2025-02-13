using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands;

public record DeleteBlogPostCommand(Guid BlogPostUuid, Guid AuthorUuid) : IRequest<DomainResponse<BlogPostResponseDto>>;