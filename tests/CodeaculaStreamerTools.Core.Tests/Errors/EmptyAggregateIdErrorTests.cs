namespace CodeaculaStreamerTools.Core.Tests.Errors;

using CodeaculaStreamerTools.Core.Common.Errors;

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
