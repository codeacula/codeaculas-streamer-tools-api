namespace Codeacula.Core.Tests.Services;

using System.Net;
using System.Text.Json;
using Codeacula.Core.Common.Errors;
using Codeacula.Core.Common.Services;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Core.Domain.Twitch.Services;
using Moq;

public class TwitchServiceTests
{
  private readonly Mock<IHttpClientWrapper> mockHttpClientWrapper;
  private readonly OAuthConfigSettings settings;
  private readonly TwitchService service;

  public TwitchServiceTests()
  {
    mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
    settings = new OAuthConfigSettings
    {
      ClientId = "test-client-id",
      ClientSecret = "test-secret",
      RedirectUri = "http://localhost",
    };

    service = new TwitchService(mockHttpClientWrapper.Object, settings);
  }

  [Fact]
  public async Task Authorize_ValidResponse_ReturnsOAuthResponseAsync()
  {
    // Arrange
    var oauthResponse = new
    {
      access_token = "valid-token",
      refresh_token = "valid-refresh",
      expires_in = 3600,
      error = string.Empty,
    };
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new StringContent(JsonSerializer.Serialize(oauthResponse)),
    };

    _ = mockHttpClientWrapper
        .Setup(x => x.PostAsync(It.IsAny<Uri>(), It.IsAny<HttpContent>()))
        .ReturnsAsync(response);

    // Act
    var result = await service.AuthorizeAsync("test-code");

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal("valid-token", result.Value.AccessToken);
    Assert.Equal("valid-refresh", result.Value.RefreshToken);
  }

  [Fact]
  public async Task Authorize_ErrorResponse_ReturnsHttpRequestFailedErrorAsync()
  {
    // Arrange
    var oauthResponse = new
    {
      access_token = (string)null!,
      refresh_token = (string)null!,
      expires_in = (int?)null,
      error = "invalid_code",
    };
    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
    {
      Content = new StringContent(JsonSerializer.Serialize(oauthResponse)),
    };

    _ = mockHttpClientWrapper
        .Setup(x => x.PostAsync(It.IsAny<Uri>(), It.IsAny<HttpContent>()))
        .ReturnsAsync(response);

    // Act
    var result = await service.AuthorizeAsync("invalid-code");

    // Assert
    Assert.False(result.IsSuccess);
    _ = Assert.IsType<HttpRequestFailedError>(result.Error);
  }

  [Fact]
  public async Task GetCurrentUser_ValidResponse_ReturnsTwitchUserInfoAsync()
  {
    // Arrange
    var userInfo = new
    {
      data = new[]
        {
            new
            {
                id = "123",
                login = "testUser",
                display_name = "TestUser",
                broadcaster_type = string.Empty,
                description = "Test Description",
                profile_image_url = "http://example.com/image.jpg",
                offline_image_url = "http://example.com/offline.jpg",
                email = "test@example.com",
                type = "user",
            },
        },
    };
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new StringContent(JsonSerializer.Serialize(userInfo)),
    };

    _ = mockHttpClientWrapper
        .Setup(x => x.GetAsync(It.IsAny<Uri>()))
        .ReturnsAsync(response);

    // Act
    service.SetTokens("test-token", "test-refresh");
    var result = await service.GetCurrentUserAsync();

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal("123", result.Value.Id);
    Assert.Equal("testUser", result.Value.Login);
  }
}
