namespace Codeacula.Core.Tests.Exceptions;

using Codeacula.Core.Domain.Authentication.Exceptions;

public class UnableToWriteToCacheExceptionTests
{
  [Fact]
  public void DefaultMessage_ShouldReturnDefaultMessage()
  {
    // Arrange
    var error = new UnableToWriteToCacheException();

    // Act
    var result = error.Message;

    // Assert
    Assert.Equal("Unable to write to cache", result);
  }

  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";
    var error = new UnableToWriteToCacheException(customMessage);

    // Act
    var result = error.Message;

    // Assert
    Assert.Equal(customMessage, result);
  }
}
