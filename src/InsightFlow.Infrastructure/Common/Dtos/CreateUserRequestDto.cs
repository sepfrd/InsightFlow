namespace InsightFlow.Infrastructure.Common.Dtos;

public record CreateUserRequestDto(
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName);