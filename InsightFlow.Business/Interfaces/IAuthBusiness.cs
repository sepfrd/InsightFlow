using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;

namespace InsightFlow.Business.Interfaces;

public interface IAuthBusiness
{
    Task<CustomResponse<string>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);

    string GetSignedInUserExternalId();
}