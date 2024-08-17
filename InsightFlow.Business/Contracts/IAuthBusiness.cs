using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;

namespace InsightFlow.Business.Contracts;

public interface IAuthBusiness
{
    Task<CustomResponse<string>> LoginAsync(LoginDto login, CancellationToken cancellationToken = default);
}