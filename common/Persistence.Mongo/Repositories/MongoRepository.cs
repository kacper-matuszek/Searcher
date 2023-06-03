using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Searcher.Application.Abstractions;

namespace Searcher.Persistence.Mongo.Repositories;

public abstract class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IKey<TKey>
    where TKey : notnull
{
    protected MongoRepository(IMongoDatabase database)
    {
        Collection = database.GetCollection<TEntity>(CollectionName);
        Projection = Builders<TEntity>.Projection.Combine();
        Sort = Builders<TEntity>.Sort.Combine();
    }

    protected abstract string CollectionName { get; }
    protected IMongoCollection<TEntity> Collection { get; }
    protected ProjectionDefinition<TEntity> Projection { get; set; }
    protected SortDefinition<TEntity> Sort { get; set; }

    public virtual async Task<TEntity?> Get(TKey key) =>
        await Get(e => e.Key.Equals(key)).ConfigureAwait(false);

    public virtual async Task<TEntity?> Get(Expression<Func<TEntity, bool>> condition)
    {
        var entity = await Collection
            .Find(condition)
            .Project<TEntity>(Projection)
            .Sort(Sort)
            .SingleOrDefaultAsync()
            .ConfigureAwait(false);
        return entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetMany(IEnumerable<TKey> keys) =>
        await GetMany(e => keys.Contains(e.Key)).ConfigureAwait(false);

    public virtual async Task<IReadOnlyList<TEntity>> GetMany(Expression<Func<TEntity, bool>> condition)
    {
        var entities = await Collection
            .Find(condition)
            .Project<TEntity>(Projection)
            .Sort(Sort)
            .ToListAsync()
            .ConfigureAwait(false);

        return entities;
    }

    public virtual async Task Add(TEntity entity) =>
        await Collection
            .InsertOneAsync(entity)
            .ConfigureAwait(false);

    public virtual async Task AddMany(IReadOnlyCollection<TEntity> entities)
    {
        if (entities.Count == 0)
            return;

        await Collection.InsertManyAsync(
            entities, new InsertManyOptions
            {
                IsOrdered = true
            }).ConfigureAwait(false);
    }

    public virtual async Task Update(TEntity entity) =>
        await Collection
            .ReplaceOneAsync(e => e.Key.Equals(entity.Key), entity)
            .ConfigureAwait(false);

    public virtual async Task<bool> Exists(TKey key) =>
        await Exists(e => e.Key.Equals(key)).ConfigureAwait(false);

    public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> condition) =>
        await Collection
            .Find(condition)
            .AnyAsync()
            .ConfigureAwait(false);

    public virtual Task Delete(TEntity entity) =>
        Collection.DeleteOneAsync(e => e.Key.Equals(entity.Key));

    public virtual Task DeleteMany(IReadOnlyCollection<TEntity> entities)
    {
        var keys = entities.Select(e => e.Key).ToHashSet();
        return Collection.DeleteManyAsync(e => keys.Contains(e.Key));
    }

    public virtual Task UpdateMany(IReadOnlyCollection<TEntity> entities)
    {
        if (entities.Count == 0)
            return Task.CompletedTask;

        var toUpdate = new List<WriteModel<TEntity>>();
        var filterBuilder = Builders<TEntity>.Filter;
        foreach (var entity in entities)
        {
            var filter = filterBuilder.Where(e => e.Key.Equals(entity.Key));
            toUpdate.Add(new ReplaceOneModel<TEntity>(filter, entity));
        }

        return Collection.BulkWriteAsync(toUpdate, new BulkWriteOptions { IsOrdered = true });
    }

    public virtual async Task Delete(TKey key) =>
        await Collection
            .DeleteOneAsync(e => e.Key.Equals(key))
            .ConfigureAwait(false);
}