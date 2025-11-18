using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Uuid,
    string NewEmail,
    string NewFirstName,
    string NewLastName)
    : ICommand<DomainResponse<UserResponseDto>>;