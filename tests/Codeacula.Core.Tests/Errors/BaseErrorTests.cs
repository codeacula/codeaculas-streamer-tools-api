namespace Codeacula.Core.Tests.Errors;

using Codeacula.Core.Common.Errors;

public class BaseErrorTests
{
  private sealed record TestError(string Message) : BaseError(Message);

  [Fact]
  public void ImplicitConversion_ShouldReturnMessage()
  {
    // Arrange
    const string errorMessage = "Test error message";

    // Act
    string result = new TestError(errorMessage);

    // Assert
    Assert.Equal(errorMessage, result);
  }
}
