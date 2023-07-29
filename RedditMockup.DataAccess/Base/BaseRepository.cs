using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.BaseEntities;
using RedditMockup.Model.Entities;
using Sieve.Models;
using Sieve.Services;

namespace RedditMockup.DataAccess.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntityWithGuid
{
    #region [Fields]

    private readonly DbSet<T> _dbSet;

    private readonly ISieveProcessor _processor;

    private readonly IMongoCollection<MongoGuidId> _mongoDbCollection;

    #endregion

    #region [Constructor]

    protected BaseRepository(RedditMockupContext context, ISieveProcessor processor, IOptions<MongoDbSettings> mongoDbSettings)
    {
        _processor = processor;

        _dbSet = context.Set<T>();

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "NoK8S")
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _mongoDbCollection = mongoDatabase.GetCollection<MongoGuidId>(mongoDbSettings.Value.CollectionName);
        }

    }

    #endregion

    #region [Methods]

    public async Task<T> CreateAsync(T t, CancellationToken cancellationToken = default)
    {
        var createdEntity = (await _dbSet.AddAsync(t, cancellationToken)).Entity;

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "NoK8S")
        {
            var guidId = new MongoGuidId(createdEntity.Guid, createdEntity.Id);
            await _mongoDbCollection.InsertOneAsync(guidId, cancellationToken: cancellationToken);
        }

        return createdEntity;
    }

    public async Task<List<T>> GetAllAsync(SieveModel sieveModel, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking();
        if (include != null)
            query = include(query);

        return await _processor.Apply(sieveModel, query).ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking().Where(t => t.Id == id);

        if (include != null)
        {
            query = include(query);
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetByGuidAsync(Guid guid, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking().Where(t => t.Guid == guid);

        if (include != null)
        {
            query = include(query);
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<int?> GetIdByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var guidIdCursor = await _mongoDbCollection.FindAsync(x => x.Guid == guid, cancellationToken: cancellationToken);

        if (guidIdCursor is null)
        {
            return null;
        }

        var guidId = await guidIdCursor.FirstAsync(cancellationToken);

        return guidId.Id;
    }

    public T Update(T t)
    {
        t.LastUpdated = DateTime.Now;

        return _dbSet.Update(t).Entity;
    }

    public T Delete(T t)
    {
        var deletedEntity = _dbSet.Remove(t).Entity;

        _mongoDbCollection.DeleteOne(x => x.Guid == deletedEntity.Guid);

        return deletedEntity;
    }

    #endregion
}