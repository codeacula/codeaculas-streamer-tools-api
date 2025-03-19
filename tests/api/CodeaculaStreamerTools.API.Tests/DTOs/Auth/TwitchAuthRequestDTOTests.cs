namespace CodeaculaStreamerTools.API.Tests.DTOs.Auth;

using CodeaculaStreamerTools.Core.Domain.Twitch.DTOs;

public class TwitchAuthRequestDTOTests
{
  [Fact]
  public void TwitchAuthRequestDTO_Should_Have_Required_Properties()
  {
    // Arrange
    var dto = new TwitchAuthRequestDTO
    {
      Code = "testCode",
      State = "testState",
    };

    // Act & Assert
    Assert.Equal("testCode", dto.Code);
    Assert.Equal("testState", dto.State);
  }
}
