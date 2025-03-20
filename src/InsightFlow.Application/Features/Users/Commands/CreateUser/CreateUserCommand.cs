using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string PasswordHash,
    string Email,
    string FirstName,
    string LastName,
    string[]? AdditionalRoles = null)
    : IRequest<DomainResponse<UserResponseDto>>;