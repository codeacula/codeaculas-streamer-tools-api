namespace Codeacula.Core.Tests.Errors;

using Codeacula.Core.Common.Errors;

public class EmptyAggregateIdErrorTests
{
  [Fact]
  public void CustomMessage_ShouldReturnCustomMessage()
  {
    // Arrange
    const string customMessage = "Custom error message";

    // Act
    string result = new EmptyAggregateIdError(customMessage);

    // Assert
    Assert.Equal(customMessage, result);
  }
}
