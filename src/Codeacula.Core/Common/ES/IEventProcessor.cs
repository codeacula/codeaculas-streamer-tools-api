namespace Codeacula.Core.Common.ES;

using Codeacula.Core.Common.Results;

public interface IEventProcessor<TEntity, TAggregateEvent>
  where TEntity : new()
  where TAggregateEvent : IAggregateEvent, new()
{
  Task<OperationResult<bool>> ApplyEventAsync(IAggregateRoot<TEntity> aggregateRoot, TAggregateEvent providedEvent);

  Task<OperationResult<bool>> ApplyEventsAsync(IAggregateRoot<TEntity> aggregateRoot, IEnumerable<TAggregateEvent> providedEvents);
}
