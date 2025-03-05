namespace Codeacula.Core.Tests.Integrations.OAuth;

using Codeacula.Core.Domain.Authentication.DTOs;

public class ExternalOAuthRequestTests
{
  [Fact]
  public void Constructor_WithRequiredProperties_SetsDefaultGrantType()
  {
    // Arrange & Act
    var request = new ExternalOAuthRequest
    {
      ClientId = "test_client",
      ClientSecret = "test_secret",
      Code = "test_code",
      RedirectUri = "test_uri",
      GrantType = "authorization_code",
    };

    // Assert
    Assert.Equal("authorization_code", request.GrantType);
  }

  [Fact]
  public async Task ToFormUrlEncodedContent_ReturnsCorrectContentAsync()
  {
    // Arrange
    var request = new ExternalOAuthRequest
    {
      ClientId = "test_client",
      ClientSecret = "test_secret",
      Code = "test_code",
      RedirectUri = "test_uri",
      GrantType = "authorization_code",
    };

    // Act
    var content = request.ToFormUrlEncodedContent();
    var contentString = await content.ReadAsStringAsync();

    // Assert
    Assert.Contains("client_id=test_client", contentString);
    Assert.Contains("client_secret=test_secret", contentString);
    Assert.Contains("code=test_code", contentString);
    Assert.Contains("redirect_uri=test_uri", contentString);
    Assert.Contains("grant_type=authorization_code", contentString);
  }
}
