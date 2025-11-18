using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.Users.Queries.GetSingleUser;

public record GetSingleUserQuery(Guid Uuid) : IQuery<DomainResponse<UserResponseDto>>;