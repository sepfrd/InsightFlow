using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string PasswordHash,
    string Email,
    string FirstName,
    string LastName,
    string[]? AdditionalRoles = null)
    : ICommand<DomainResponse<UserResponseDto>>;