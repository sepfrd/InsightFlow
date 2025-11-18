using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Features.Users.Queries.GetUserIdByUserUuid;

public record GetUserIdByUserUuidQuery(Guid Uuid) : IQuery<DomainResponse<long?>>;