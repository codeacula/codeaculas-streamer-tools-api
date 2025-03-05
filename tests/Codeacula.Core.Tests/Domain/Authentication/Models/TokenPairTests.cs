namespace Codeacula.Core.Tests.Domain.Authentication.Models;

using Codeacula.Core.Domain.Authentication.Models;

public class TokenPairTests
{
  [Fact]
  public void Constructor_InitializesProperties()
  {
    // Arrange
    const string accessToken = "test-access-token";
    const string refreshToken = "test-refresh-token";

    // Act
    var tokenPair = new TokenPair(accessToken, refreshToken);

    // Assert
    Assert.Equal(accessToken, tokenPair.AccessToken);
    Assert.Equal(refreshToken, tokenPair.RefreshToken);
  }

  [Fact]
  public void Records_ImplementValueEquality()
  {
    // Arrange
    var tokenPair1 = new TokenPair("access1", "refresh1");
    var tokenPair2 = new TokenPair("access1", "refresh1");
    var tokenPair3 = new TokenPair("access2", "refresh2");

    // Assert
    Assert.Equal(tokenPair1, tokenPair2);
    Assert.NotEqual(tokenPair1, tokenPair3);
  }
}
