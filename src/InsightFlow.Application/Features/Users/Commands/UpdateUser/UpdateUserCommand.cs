using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Uuid,
    string NewEmail,
    string NewFirstName,
    string NewLastName)
    : IRequest<DomainResponse<UserResponseDto>>;