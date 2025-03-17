using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Dtos;

namespace InsightFlow.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<DomainResponse<string>> AuthenticateAsync(LoginDto loginDto, CancellationToken cancellationToken = default);

    string GetSignedInUserUuid();

    bool IsSignedIn();
}