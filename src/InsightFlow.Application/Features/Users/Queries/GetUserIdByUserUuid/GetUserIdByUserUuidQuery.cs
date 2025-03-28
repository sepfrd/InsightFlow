using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.Users.Queries.GetUserIdByUserUuid;

public record GetUserIdByUserUuidQuery(Guid Uuid) : IRequest<DomainResponse<long?>>;