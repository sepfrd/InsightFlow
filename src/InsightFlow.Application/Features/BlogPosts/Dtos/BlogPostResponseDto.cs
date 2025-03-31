using InsightFlow.Application.Features.Users.Dtos;

namespace InsightFlow.Application.Features.BlogPosts.Dtos;

public record BlogPostResponseDto(
    Guid Uuid,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Title,
    string Body,
    UserResponseDto Author);