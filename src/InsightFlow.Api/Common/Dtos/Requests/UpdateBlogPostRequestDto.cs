namespace InsightFlow.Api.Common.Dtos.Requests;

public record UpdateBlogPostRequestDto(Guid BlogPostUuid, string NewTitle, string NewBody);