namespace CodeaculaStreamerTools.Core.Common.ES;

public interface IAggregateRoot<T>
  where T : new()
{
  Guid Id { get; }

  int CurrentVersion { get; }

  IEnumerable<IAggregateEvent> AggregateEvents { get; }
}
