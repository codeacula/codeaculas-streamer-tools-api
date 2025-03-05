namespace Codeacula.API.Tests;

using Codeacula.API.Responses;

public class ApiResponseTest
{
  [Fact]
  public void DefaultConstructor_SetsExpectedDefaults()
  {
    // Arrange & Act
    var response = new ApiResponse<string>();

    // Assert
    Assert.False(response.Success);
    Assert.Null(response.Data);
    Assert.Null(response.ErrorCode);
    Assert.Null(response.ErrorMessage);
    Assert.True((DateTime.UtcNow - response.Timestamp).TotalSeconds < 1);
  }

  [Fact]
  public void DataConstructor_SetsSuccessAndData()
  {
    // Arrange
    const string testData = "test";

    // Act
    var response = new ApiResponse<string>(testData);

    // Assert
    Assert.True(response.Success);
    Assert.Equal(testData, response.Data);
    Assert.Null(response.ErrorCode);
    Assert.Null(response.ErrorMessage);
  }

  [Fact]
  public void ErrorConstructor_SetsErrorProperties()
  {
    // Arrange
    const string errorCode = "404";
    const string errorMessage = "Not Found";

    // Act
    var response = new ApiResponse<string>(errorCode, errorMessage);

    // Assert
    Assert.False(response.Success);
    Assert.Null(response.Data);
    Assert.Equal(errorCode, response.ErrorCode);
    Assert.Equal(errorMessage, response.ErrorMessage);
  }

  [Fact]
  public void StaticOk_ReturnsSuccessResponse()
  {
    // Arrange
    const int testData = 42;

    // Act
    var response = ApiResponse.Ok(testData);

    // Assert
    Assert.True(response.Success);
    Assert.Equal(testData, response.Data);
  }

  [Fact]
  public void StaticError_ReturnsErrorResponse()
  {
    // Arrange
    const string errorMessage = "Something went wrong";

    // Act
    var response = ApiResponse.Error(errorMessage);

    // Assert
    Assert.False(response.Success);
    Assert.Equal("500", response.ErrorCode);
    Assert.Equal(errorMessage, response.ErrorMessage);
  }
}
