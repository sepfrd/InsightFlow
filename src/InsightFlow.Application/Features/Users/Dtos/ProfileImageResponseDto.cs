namespace InsightFlow.Application.Features.Users.Dtos;

public record ProfileImageResponseDto(byte[] ImageBytes, string ImageFormat, DateTime UpdatedAt);