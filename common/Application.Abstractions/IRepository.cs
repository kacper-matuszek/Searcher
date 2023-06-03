using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Searcher.Application.Abstractions;

public interface IRepository<TEntity, in TKey>
    where TEntity : class, IKey<TKey>
    where TKey : notnull
{
    Task<TEntity?> Get(TKey key);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> condition);
    Task<IReadOnlyList<TEntity>> GetMany(IEnumerable<TKey> keys);
    Task<IReadOnlyList<TEntity>> GetMany(Expression<Func<TEntity, bool>> condition);
    Task Add(TEntity entity);
    Task AddMany(IReadOnlyCollection<TEntity> entities);
    Task Update(TEntity entity);
    Task UpdateMany(IReadOnlyCollection<TEntity> entities);
    Task<bool> Exists(TKey key);
    Task<bool> Exists(Expression<Func<TEntity, bool>> condition);
    Task Delete(TEntity entity);
    Task DeleteMany(IReadOnlyCollection<TEntity> entities);
    Task Delete(TKey key);
}
