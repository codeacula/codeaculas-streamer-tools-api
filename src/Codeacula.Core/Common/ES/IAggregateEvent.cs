namespace Codeacula.Core.Common.ES;

public interface IAggregateEvent
{
  Guid AggregateEventId { get; }

  Guid AggregateId { get; }

  string EventType { get; }

  int Version { get; }

  DateTime Timestamp { get; }

  IDictionary<string, object>? Metadata { get; }
}
