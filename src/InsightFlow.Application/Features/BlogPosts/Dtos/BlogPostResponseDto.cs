namespace InsightFlow.Application.Features.BlogPosts.Dtos;

public record BlogPostResponseDto(Guid Uuid, string Title, string Body, Guid AuthorUuid);