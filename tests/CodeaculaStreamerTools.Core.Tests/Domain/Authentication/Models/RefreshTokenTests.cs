namespace CodeaculaStreamerTools.Core.Tests.Domain.Authentication.Models;

using CodeaculaStreamerTools.Core.Domain.Authentication.Models;

public class RefreshTokenTests
{
  [Fact]
  public void DefaultConstructor_SetsExpectedDefaults()
  {
    // Arrange & Act
    var refreshToken = new RefreshToken();

    // Assert
    Assert.NotNull(refreshToken);
  }
}
