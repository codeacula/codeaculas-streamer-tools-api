namespace Codeacula.Core.Tests.Errors;

using Codeacula.Core.Domain.Authentication.Errors;

public class StateStringMissingErrorTests
{
  [Fact]
  public void DefaultMessage_ShouldReturnDefaultMessage()
  {
    // Arrange

    // Act
    string result = new StateStringMissingError();

    // Assert
    Assert.Equal("The requested state string couldn't be found in cache", result);
  }

  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new StateStringMissingError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
