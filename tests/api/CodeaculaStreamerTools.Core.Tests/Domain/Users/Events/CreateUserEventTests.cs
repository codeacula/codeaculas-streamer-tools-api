namespace CodeaculaStreamerTools.Core.Tests.Domain.Users.Events;

using CodeaculaStreamerTools.Core.Domain.Users.Events;

public class CreateUserEventTests
{
  [Fact]
  public void Constructor_ShouldSetEventType()
  {
    // Act
    var createUserEvent = new CreateUserEvent
    {
      AggregateId = Guid.NewGuid(),
      AggregateType = "User",
      EventType = nameof(CreateUserEvent),
      Data = "SampleData",
      Version = 1,
    };

    // Assert
    Assert.Equal(nameof(CreateUserEvent), createUserEvent.EventType);
  }

  [Fact]
  public void Constructor_ShouldGenerateId()
  {
    // Act
    var createUserEvent = new CreateUserEvent
    {
      AggregateId = Guid.NewGuid(),
      AggregateType = "User",
      EventType = nameof(CreateUserEvent),
      Data = "SampleData",
      Version = 1,
    };

    // Assert
    Assert.NotEqual(Guid.Empty, createUserEvent.AggregateId);
  }
}
