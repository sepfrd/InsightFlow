using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;
using Sieve.Services;

namespace RedditMockup.DataAccess.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{

    #region [Fields]

    private readonly DbSet<T> _dbSet;

    private readonly ISieveProcessor _processor;

    #endregion

    #region [Constructor]

    public BaseRepository(RedditMockupContext context, ISieveProcessor processor)
    {
        _processor = processor;
        _dbSet = context.Set<T>();
    }

    #endregion

    #region [Methods]

    public async Task<T> CreateAsync(T t, CancellationToken cancellationToken = new()) =>
        (await _dbSet.AddAsync(t, cancellationToken)).Entity;

    public async Task<List<T>> LoadAllAsync(SieveModel sieveModel,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        CancellationToken cancellationToken = new())
    {
        var query = _dbSet.AsNoTracking();
        if (include != null)
            query = include(query);

        return await _processor.Apply(sieveModel, query).ToListAsync(cancellationToken);
    }

    public async Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken = new()) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public T Update(T t)
    {
        t.LastUpdated = DateTime.Now;

        return _dbSet.Update(t).Entity;
    }

    public T Delete(T t) =>
        _dbSet.Remove(t).Entity;

    #endregion

}