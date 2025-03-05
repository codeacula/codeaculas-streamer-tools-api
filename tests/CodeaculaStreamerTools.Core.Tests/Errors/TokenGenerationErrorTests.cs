namespace CodeaculaStreamerTools.Core.Tests.Errors;

using CodeaculaStreamerTools.Core.Domain.Twitch.Errors;

public class TokenGenerationErrorTests
{
  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new TokenGenerationError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
