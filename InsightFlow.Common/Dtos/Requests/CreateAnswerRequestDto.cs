namespace InsightFlow.Common.Dtos.Requests;

public record CreateAnswerRequestDto(Guid QuestionGuid, string Title, string Description);