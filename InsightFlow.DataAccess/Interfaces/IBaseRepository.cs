using InsightFlow.DataAccess.Dtos;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;

namespace InsightFlow.DataAccess.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    Task<PagedEntitiesResponseDto<T>> GetAllAsync(
        SieveModel sieveModel,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(
        int id,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        CancellationToken cancellationToken = default);

    Task<T?> GetByGuidAsync(
        Guid guid,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        CancellationToken cancellationToken = default);

    T Delete(T entity);
}