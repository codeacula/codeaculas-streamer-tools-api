namespace CodeaculaStreamerTools.Core.Tests.Domain.Authentication.Models;

using CodeaculaStreamerTools.Core.Domain.Authentication.Models;

public class OAuthRequestTests
{
  [Fact]
  public void Properties_ShouldBeSettable()
  {
    // Arrange & Act
    var request = new OAuthRequest
    {
      Code = "test-code",
      State = "test-state",
    };

    // Assert
    Assert.Equal("test-code", request.Code);
    Assert.Equal("test-state", request.State);
  }
}
