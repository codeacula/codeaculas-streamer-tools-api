namespace CodeaculaStreamerTools.Core.Tests.Domain.Authentication.Models;

using CodeaculaStreamerTools.Core.Domain.Authentication.Models;

public class OAuthResponseTests
{
  [Fact]
  public void Properties_ShouldBeSettable()
  {
    // Arrange
    var expiresOn = DateTime.UtcNow.AddHours(1);

    // Act
    var response = new OAuthResponse
    {
      AccessToken = "test-access-token",
      RefreshToken = "test-refresh-token",
      ExpiresOn = expiresOn,
    };

    // Assert
    Assert.Equal("test-access-token", response.AccessToken);
    Assert.Equal("test-refresh-token", response.RefreshToken);
    Assert.Equal(expiresOn, response.ExpiresOn);
  }
}
