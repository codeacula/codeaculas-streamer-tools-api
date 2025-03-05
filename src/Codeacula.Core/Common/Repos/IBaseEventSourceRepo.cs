namespace Codeacula.Core.Common.Repos;

using Codeacula.Core.Common.ES;
using Codeacula.Core.Common.Results;

/// <summary>
/// Represents the base interface for event sourcing repositories.
/// </summary>
public interface IBaseEventSourceRepo
{
  Task<OperationResult<int>> DeleteEventsByAggregateIdAsync(Guid aggregateId);

  Task<OperationResult<IReadOnlyList<IncomingEvent>>> GetEventsByAggregateIdAsync(Guid aggregateId);

  Task<OperationResult<IncomingEvent?>> GetLatestEventByAggregateIdAsync(Guid aggregateId);

  Task<OperationResult<int>> SaveAsync(IncomingEvent ev);

  Task<OperationResult<bool>> ValidateEventAsync(IncomingEvent ev);
}
