namespace CodeaculaStreamerTools.Core.Common.Repos;

using CodeaculaStreamerTools.Core.Common.ES;
using CodeaculaStreamerTools.Core.Common.Results;

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
