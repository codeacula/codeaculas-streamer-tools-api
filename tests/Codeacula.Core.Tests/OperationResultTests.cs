namespace Codeacula.Core.Tests;
using Codeacula.Core.Common.Errors;
using Codeacula.Core.Common.Results;

public class OperationResultTests
{
  private sealed record TestError : BaseError
  {
    public TestError(string message)
      : base(message)
    {
    }
  }

  [Fact]
  public void Success_WithValue_ShouldCreateSuccessResult()
  {
    // Arrange
    const string testValue = "test";

    // Act
    var result = new SuccessResult<string>(testValue);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.False(result.IsError);
    Assert.Equal(testValue, result.Value);
    Assert.Null(result.Error);
  }

  [Fact]
  public void Failure_WithError_ShouldCreateFailureResult()
  {
    // Arrange
    var error = new TestError("test error");

    // Act
    var result = new FailureResult<string>(error);

    // Assert
    Assert.False(result.IsSuccess);
    Assert.True(result.IsError);
    Assert.Equal(error, result.Error);
    Assert.Equal(error.Message, result.ErrorMessage);
  }

  [Fact]
  public void ImplicitOperator_FromValue_ShouldCreateSuccessResult()
  {
    // Arrange
    const string testValue = "test";

    // Act
    OperationResult<string> result = testValue;

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(testValue, result.Value);
  }

  [Fact]
  public void ImplicitOperator_FromError_ShouldCreateFailureResult()
  {
    // Arrange
    var error = new TestError("test error");

    // Act
    OperationResult<string> result = error;

    // Assert
    Assert.True(result.IsError);
    Assert.Equal(error, result.Error);
  }

  [Fact]
  public void ImplicitOperator_ToValue_ShouldExtractValue()
  {
    // Arrange
    const string testValue = "test";

    // Act
    string extractedValue = (OperationResult<string>)testValue;

    // Assert
    Assert.Equal(testValue, extractedValue);
  }

  [Fact]
  public void Value_OnFailure_ShouldThrowInvalidOperationException()
  {
    // Arrange
    OperationResult<string> result = new TestError("test error");

    // Act & Assert
    _ = Assert.Throws<InvalidOperationException>(() => result.Value);
  }

  [Fact]
  public void ErrorMessage_OnSuccess_ShouldThrowInvalidOperationException()
  {
    // Arrange
    OperationResult<string> result = "test";

    // Act & Assert
    _ = Assert.Throws<InvalidOperationException>(() => result.ErrorMessage);
  }

  [Fact]
  public void ErrorMessage_WithNullError_ShouldThrowInvalidOperationException()
  {
    // Arrange
    var result = new FailureResult<string>(null!);

    // Act & Assert
    _ = Assert.Throws<InvalidOperationException>(() => result.ErrorMessage);
  }

  [Fact]
  public void ImplicitOperator_FromFailureToValue_ShouldThrowInvalidOperationException()
  {
    // Arrange
    OperationResult<string> result = new TestError("test error");

    // Act & Assert
    _ = Assert.Throws<InvalidOperationException>(() =>
    {
      string value = result;
    });
  }
}
