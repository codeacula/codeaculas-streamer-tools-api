namespace CodeaculaStreamerTools.Core.Tests.Infrastructure.Configuration;

using CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;

public class OAuthConfigSettingsTests
{
  [Fact]
  public void Properties_ShouldBeSettable()
  {
    // Arrange
    var settings = new OAuthConfigSettings
    {
      ClientId = "test-client",
      ClientSecret = "test-secret",
      RedirectUri = "http://localhost",
      GrantType = "custom-grant",
    };

    // Assert
    Assert.Equal("test-client", settings.ClientId);
    Assert.Equal("test-secret", settings.ClientSecret);
    Assert.Equal("http://localhost", settings.RedirectUri);
    Assert.Equal("custom-grant", settings.GrantType);
  }

  [Fact]
  public void ToExternalOAuthRequest_ShouldMapCorrectly()
  {
    // Arrange
    var settings = new OAuthConfigSettings
    {
      ClientId = "test-client",
      ClientSecret = "test-secret",
      RedirectUri = "http://localhost",
      GrantType = "custom-grant",
    };

    // Act
    var request = settings.ToExternalOAuthRequest("test-code");

    // Assert
    Assert.Equal(settings.ClientId, request.ClientId);
    Assert.Equal(settings.ClientSecret, request.ClientSecret);
    Assert.Equal(settings.RedirectUri, request.RedirectUri);
    Assert.Equal(settings.GrantType, request.GrantType);
    Assert.Equal("test-code", request.Code);
  }
}
