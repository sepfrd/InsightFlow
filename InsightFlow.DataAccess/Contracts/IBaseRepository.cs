using InsightFlow.DataAccess.Dtos;
using InsightFlow.Model.BaseEntities;
using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;

namespace InsightFlow.DataAccess.Contracts;

public interface IBaseRepository<T> where T : BaseEntityWithGuid
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

    Task<PagedEntitiesResponseDto<T>> GetAllAsync(SieveModel sieveModel, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default);

    Task<T?> GetByGuidAsync(Guid guid, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default);

    T Update(T entity);

    T Delete(T entity);
}