namespace InsightFlow.Application.Features.Users.Dtos;

public record UserResponseDto(Guid Uuid, string Username, string Email, string FullName);