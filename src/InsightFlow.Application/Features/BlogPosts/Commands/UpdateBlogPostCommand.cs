using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

namespace InsightFlow.Application.Features.BlogPosts.Commands;

public record UpdateBlogPostCommand(Guid BlogPostUuid, string NewTitle, string NewDescription, Guid AuthorUuid) :
    IRequest<DomainResponse<BlogPostResponseDto>>;