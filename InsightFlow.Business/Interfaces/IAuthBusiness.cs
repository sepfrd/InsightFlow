using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;

namespace InsightFlow.Business.Interfaces;

public interface IAuthBusiness
{
    Task<CustomResponse<string>> LoginAsync(LoginDto login, CancellationToken cancellationToken = default);
}