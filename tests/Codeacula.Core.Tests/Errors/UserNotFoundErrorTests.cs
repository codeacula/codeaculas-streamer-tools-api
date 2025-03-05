namespace Codeacula.Core.Tests.Errors;

using Codeacula.Core.Domain.Users.Errors;

public class UserNotFoundErrorTests
{
  [Fact]
  public void DefaultMessage_ShouldReturnDefaultMessage()
  {
    // Arrange

    // Act
    string result = new UserNotFoundError();

    // Assert
    Assert.Equal("The requested user could not be found", result);
  }

  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new UserNotFoundError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
