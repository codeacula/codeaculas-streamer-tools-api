namespace CodeaculaStreamerTools.Core.Common.ES;

using CodeaculaStreamerTools.Core.Common.Results;

/// <summary>
/// Represents a store for aggregates. A store is responsible for loading and saving, as well as determining
/// when to snapshot. The store interacts with a different data stores in order to save events and snapshots.
/// </summary>
/// <typeparam name="TEntity">The type of entity the aggregate store represents.</typeparam>
public interface IAggregateStore<TEntity>
  where TEntity : new()
{
  Task<OperationResult<IAggregateRoot<TEntity>>> GetByIdAsync(Guid id);

  Task<OperationResult<IAggregateRoot<TEntity>>> SaveAsync(IAggregateRoot<TEntity> aggregateRoot);
}
