namespace CodeaculaStreamerTools.Core.Tests.Errors;

using CodeaculaStreamerTools.Core.Domain.Twitch.Errors;

public class TwitchNoUserDataTests
{
  [Fact]
  public void DefaultMessage_ShouldReturnDefaultMessage()
  {
    // Arrange

    // Act
    string result = new TwitchNoUserData();

    // Assert
    Assert.Equal("Twitch failed to provide user data", result);
  }

  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new TwitchNoUserData(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
