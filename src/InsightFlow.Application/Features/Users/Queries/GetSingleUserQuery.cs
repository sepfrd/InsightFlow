using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.Users.Queries;

public record GetSingleUserQuery(Guid Uuid) : IRequest<DomainResponse<UserResponseDto>>;