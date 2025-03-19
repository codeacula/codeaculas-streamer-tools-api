namespace CodeaculaStreamerTools.Core.Common.CQRS;

public record QueryBase
{
  public Guid Id { get; init; } = Guid.NewGuid();

  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
