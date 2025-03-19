namespace CodeaculaStreamerTools.Core.Common.CQRS;

public interface ICommand<TResult>
{
  Guid Id { get; }

  long Version { get; }

  DateTime CreatedAt { get; }
}
