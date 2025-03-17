using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.Users.Commands;

public record UpdateUserInformationCommand(
    Guid Uuid,
    string NewEmail,
    string NewFirstName,
    string NewLastName)
    : IRequest<DomainResponse<UserResponseDto>>;