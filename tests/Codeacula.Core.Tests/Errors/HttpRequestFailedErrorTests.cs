namespace Codeacula.Core.Tests.Errors;

using Codeacula.Core.Common.Errors;

public class HttpRequestFailedErrorTests
{
  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new HttpRequestFailedError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
