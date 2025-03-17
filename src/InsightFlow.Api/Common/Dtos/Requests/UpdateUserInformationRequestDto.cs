namespace InsightFlow.Api.Common.Dtos.Requests;

public record UpdateUserInformationRequestDto(
    string NewEmail,
    string NewFirstName,
    string NewLastName);