namespace Codeacula.Core.Tests.Infrastructure.Network.Twitch;

using Codeacula.Core.Domain.Twitch.DTOs;

public class TwitchApiResultTests
{
  [Fact]
  public void Constructor_InitializesProperties()
  {
    // Arrange
    var data = new[]
    {
      new TwitchUserInfoDTO
        {
            Id = "123",
            Login = "test",
            DisplayName = "Test",
            BroadcasterType = string.Empty,
            Description = "Test Description",
            Email = "test@example.com",
            ProfileImageUrl = "http://example.com/image.jpg",
            OfflineImageUrl = "http://example.com/offline.jpg",
            Type = "user",
        },
    };

    // Act
    var result = new TwitchApiResultDTO<TwitchUserInfoDTO>
    {
      Data = data,
    };

    // Assert
    Assert.NotNull(result.Data);
    _ = Assert.Single(result.Data);
    Assert.Equal("123", result.Data.First().Id);
  }
}
