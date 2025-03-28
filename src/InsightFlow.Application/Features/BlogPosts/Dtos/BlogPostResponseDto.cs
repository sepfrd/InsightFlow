namespace InsightFlow.Application.Features.BlogPosts.Dtos;

public record BlogPostResponseDto(Guid Uuid, DateTime CreatedAt, DateTime UpdatedAt, string Title, string Body, Guid AuthorUuid);