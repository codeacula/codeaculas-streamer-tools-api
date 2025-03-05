namespace Codeacula.Core.Tests.Errors;

using Codeacula.Core.Domain.Authentication.Errors;

public class InvalidTokenErrorTests
{
  [Fact]
  public void DefaultMessage_ShouldReturnDefaultMessage()
  {
    // Arrange

    // Act
    string result = new InvalidTokenError();

    // Assert
    Assert.Equal("The provided token was invalid.", result);
  }

  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new InvalidTokenError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
