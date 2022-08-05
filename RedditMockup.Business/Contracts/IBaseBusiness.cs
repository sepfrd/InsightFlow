using Microsoft.AspNetCore.Http;
using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Business.Contracts;

public interface IBaseBusiness<T, DTO>
{
    Task<CustomResponse?> CreateAsync(T t, CancellationToken cancellationToken);

    Task<CustomResponse<IEnumerable<DTO>>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    Task<CustomResponse?> UpdateAsync(T t, CancellationToken cancellationToken);

    Task<CustomResponse?> DeleteAsync(T t, CancellationToken cancellationToken);

    Task<CustomResponse?> CreateAsync(DTO dto, HttpContext httpContext, CancellationToken cancellationToken);

    Task<CustomResponse?> LoadByIdAsync(int id, CancellationToken cancellationToken);

    Task<CustomResponse?> UpdateAsync(int id, DTO dto, CancellationToken cancellationToken);

    Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken);

}