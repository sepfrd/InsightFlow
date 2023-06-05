using Sieve.Models;

namespace RedditMockup.Api.Contracts;

public interface IBaseController<TEntity, in TDto>
{
    Task<TEntity?> CreateAsync(TDto dto, CancellationToken cancellationToken);

    Task<List<TEntity>?> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<TEntity?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken);

    Task<TEntity?> UpdateAsync(TDto dto, CancellationToken cancellationToken);

    Task<TEntity?> DeleteByIdAsync(int id, CancellationToken cancellationToken);

    Task<TEntity?> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken);
}