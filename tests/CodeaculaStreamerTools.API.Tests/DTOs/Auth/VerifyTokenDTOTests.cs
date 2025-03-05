namespace CodeaculaStreamerTools.API.Tests.DTOs.Auth;

using CodeaculaStreamerTools.Core.Domain.Authentication.DTOs;

public class VerifyTokenDTOTests
{
  [Fact]
  public void VerifyTokenDTO_Should_Have_Required_Properties()
  {
    // Arrange
    var dto = new VerifyTokenDTO
    {
      Token = "test_token",
    };

    // Act & Assert
    Assert.Equal("test_token", dto.Token);
  }
}
