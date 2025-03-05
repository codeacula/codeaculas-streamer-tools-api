namespace Codeacula.Core.Domain.Users.Events;

using Codeacula.Core.Common.ES;

public record CreateUserEvent : IncomingEvent
{
  public CreateUserEvent()
  {
    EventType = nameof(CreateUserEvent);
  }
}
