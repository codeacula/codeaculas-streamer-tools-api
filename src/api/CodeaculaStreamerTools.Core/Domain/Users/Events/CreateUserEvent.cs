namespace CodeaculaStreamerTools.Core.Domain.Users.Events;

using CodeaculaStreamerTools.Core.Common.ES;

public record CreateUserEvent : IncomingEvent
{
  public CreateUserEvent()
  {
    EventType = nameof(CreateUserEvent);
  }
}
