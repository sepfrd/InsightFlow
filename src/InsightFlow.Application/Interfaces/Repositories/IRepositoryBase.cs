using System.Linq.Expressions;
using InsightFlow.Domain.Common;

namespace InsightFlow.Application.Interfaces.Repositories;

public interface IRepositoryBase<TEntity, in TKey>
    where TEntity : DomainEntity
    where TKey : IEquatable<TKey>
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task CreateManyAsync(ICollection<TEntity> entity, CancellationToken cancellationToken = default);

    Task<TEntity?> GetOneAsync(
        Expression<Func<TEntity, bool>> filter,
        bool useSplitQuery = false,
        ICollection<Expression<Func<TEntity, object?>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetOneAsync<TSorter>(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TSorter>> orderBy,
        bool useSplitQuery = false,
        bool ascendingOrder = true,
        IEnumerable<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default)
        where TSorter : IComparable<TSorter>;

    Task<TResult?> GetOneAsync<TResult>(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> subsetSelector,
        bool useSplitQuery = false,
        IEnumerable<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<TResult?> GetOneAsync<TResult, TSorter>(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> subsetSelector,
        Expression<Func<TEntity, TSorter>> orderBy,
        bool useSplitQuery = false,
        bool ascendingOrder = true,
        IEnumerable<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default)
        where TSorter : IComparable<TSorter>;

    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        bool useSplitQuery = false,
        uint page = 1, uint limit = 10,
        ICollection<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(
        bool useSplitQuery = false,
        uint page = 1, uint limit = 10,
        ICollection<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync<TSorter>(
        Expression<Func<TEntity, TSorter>> orderBy,
        bool useSplitQuery = false,
        uint page = 1,
        uint limit = 10,
        bool ascendingOrder = true,
        ICollection<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync<TSorter>(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TSorter>> orderBy,
        bool useSplitQuery = false,
        uint page = 1,
        uint limit = 10,
        bool ascendingOrder = true,
        ICollection<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TResult>> GetAllAsync<TResult, TSorter>(
        Expression<Func<TEntity, TResult>> subsetSelector,
        Expression<Func<TEntity, TSorter>> orderBy,
        bool useSplitQuery = false,
        uint page = 1,
        uint limit = 10,
        bool ascendingOrder = true,
        ICollection<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TResult>> GetAllAsync<TResult, TSorter>(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> subsetSelector,
        Expression<Func<TEntity, TSorter>> orderBy,
        bool useSplitQuery = false,
        uint page = 1,
        uint limit = 10,
        bool ascendingOrder = true,
        ICollection<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    void Update(TEntity entityToUpdate);

    void Delete(TEntity entityToDelete);

    Task<int> GetCountAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default);
}