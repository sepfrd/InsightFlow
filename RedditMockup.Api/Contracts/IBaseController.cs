using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Api.Contracts;

public interface IBaseController<DTO>

{
    Task<CustomResponse?> CreateAsync(DTO dto, CancellationToken cancellationToken);

    Task<CustomResponse<IEnumerable<DTO>>?> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    Task<CustomResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<CustomResponse?> UpdateAsync(int id, DTO dto, CancellationToken cancellationToken);

    Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken);

    void Options();
}