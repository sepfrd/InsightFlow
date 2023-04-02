using Sieve.Models;

namespace RedditMockup.Business.Contracts;

public interface IBaseBusiness<T>
{
    Task<T?> CreateAsync(T t, CancellationToken cancellationToken);

    Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<T>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    Task<T?> UpdateAsync(T t, CancellationToken cancellationToken);

    Task<T?> DeleteAsync(int id, CancellationToken cancellationToken);

    //Task<CustomResponse?> CreateAsync(DTO dto, HttpContext httpContext, CancellationToken cancellationToken);


    //Task<CustomResponse<IEnumerable<DTO>>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    //Task<CustomResponse?> UpdateAsync(int id, DTO dto, CancellationToken cancellationToken);

    //Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken);
}