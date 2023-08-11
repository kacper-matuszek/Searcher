using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Searcher.Common.Application.Abstractions;

public interface IRepository<TEntity, in TKey>
    where TEntity : class, IKey<TKey>
    where TKey : notnull
{
    Task<TEntity?> Get(TKey key, CancellationToken cancellationToken = default);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetMany(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetMany(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
    Task Add(TEntity entity);
    Task AddMany(IReadOnlyCollection<TEntity> entities);
    Task Update(TEntity entity);
    Task UpdateMany(IReadOnlyCollection<TEntity> entities);
    Task<bool> Exists(TKey key, CancellationToken cancellationToken = default);
    Task<bool> Exists(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
    Task Delete(TEntity entity);
    Task DeleteMany(IReadOnlyCollection<TEntity> entities);
    Task Delete(TKey key);
}
