namespace CodeaculaStreamerTools.Core.Common.CQRS;

public abstract record CommandBase<TResult> : ICommand<TResult>
{
  public Guid Id { get; init; } = Guid.NewGuid();

  public long Version { get; init; } = 1;

  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
