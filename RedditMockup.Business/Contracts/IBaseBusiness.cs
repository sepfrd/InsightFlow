using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;

namespace RedditMockup.Business.Contracts;

public interface IBaseBusiness<TEntity, in TDto>
{
    Task<TEntity?> CreateAsync(TDto questionDto, CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetByGuidAsync(Guid guid, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, CancellationToken cancellationToken = default);

    Task<List<TEntity>?> GetAllAsync(SieveModel sieveModel, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
    
}