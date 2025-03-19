namespace CodeaculaStreamerTools.Core.Tests.Errors;

using CodeaculaStreamerTools.Core.Common.Errors;

public class NoValueReturnedErrorTests
{
  [Fact]
  public void DefaultMessage_ShouldReturnDefaultMessage()
  {
    // Arrange

    // Act
    string result = new NoValueReturnedError();

    // Assert
    Assert.Equal("No value was returned.", result);
  }

  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new NoValueReturnedError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
