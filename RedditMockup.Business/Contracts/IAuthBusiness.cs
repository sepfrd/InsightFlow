using RedditMockup.Common.Dtos;

namespace RedditMockup.Business.Contracts;

public interface IAuthBusiness
{
    Task<CustomResponse<string>> LoginAsync(LoginDto login, CancellationToken cancellationToken = default);
}