namespace InsightFlow.Application.Common;

public record DataValidationResult(bool IsValid, IEnumerable<string> ValidationErrors);