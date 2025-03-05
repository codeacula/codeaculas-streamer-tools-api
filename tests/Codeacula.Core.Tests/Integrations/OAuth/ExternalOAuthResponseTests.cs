namespace Codeacula.Core.Tests.Integrations.OAuth;

using System.Text.Json;
using Codeacula.Core.Domain.Authentication.DTOs;

public class ExternalOAuthResponseTests
{
  [Fact]
  public void Deserialize_WithValidJson_SetsAllProperties()
  {
    // Arrange
    const string json = /*lang=json,strict*/ """
        {
            "access_token": "test_token",
            "error": "test_error",
            "expires_in": 3600,
            "message": 123,
            "refresh_token": "test_refresh",
            "scope": ["scope1", "scope2"],
            "status": 200,
            "token_type": "bearer"
        }
        """;

    // Act
    var result = JsonSerializer.Deserialize<ExternalOAuthResponse>(json);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("test_token", result.AccessToken);
    Assert.Equal("test_error", result.Error);
    Assert.Equal(3600, result.ExpiresIn);
    Assert.Equal(123, result.Message);
    Assert.Equal("test_refresh", result.RefreshToken);
    Assert.Equal(["scope1", "scope2"], result.Scope);
    Assert.Equal(200, result.Status);
    Assert.Equal("bearer", result.TokenType);
  }

  [Fact]
  public void Constructor_DefaultValues_AreCorrect()
  {
    // Arrange & Act
    var response = new ExternalOAuthResponse();

    // Assert
    Assert.Null(response.AccessToken);
    Assert.Equal(string.Empty, response.Error);
    Assert.Null(response.ExpiresIn);
    Assert.Null(response.Message);
    Assert.Null(response.RefreshToken);
    Assert.Null(response.Scope);
    Assert.Null(response.Status);
    Assert.Null(response.TokenType);
  }
}
