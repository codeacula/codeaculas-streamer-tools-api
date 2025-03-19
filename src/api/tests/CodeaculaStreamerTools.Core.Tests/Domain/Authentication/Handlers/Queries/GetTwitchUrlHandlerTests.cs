namespace CodeaculaStreamerTools.Core.Tests.Domain.Authentication.Handlers.Queries;

using CodeaculaStreamerTools.Core.Common.Errors;
using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;
using CodeaculaStreamerTools.Core.Domain.Authentication.Handlers;
using CodeaculaStreamerTools.Core.Domain.Authentication.Queries;
using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;
using CodeaculaStreamerTools.Core.Domain.Twitch.Helpers;
using Moq;

public class GetTwitchUrlHandlerTests
{
  private readonly Mock<IStateStringRepo> mockStateStringRepo;
  private readonly OAuthConfigSettings oAuthConfigSettings;
  private readonly GetTwitchUrlHandler handler;

  public GetTwitchUrlHandlerTests()
  {
    mockStateStringRepo = new Mock<IStateStringRepo>();
    oAuthConfigSettings = new OAuthConfigSettings
    {
      ClientId = "test-client-id",
      RedirectUri = "http://localhost/callback",
      ClientSecret = "test-client-secret",
    };

    handler = new GetTwitchUrlHandler(mockStateStringRepo.Object, oAuthConfigSettings);
  }

  [Fact]
  public async Task HandleAsync_WithValidState_ReturnsCorrectUrlAsync()
  {
    // Arrange
    const string testState = "test-state";
    var query = new GetTwitchUrlQuery();
    _ = mockStateStringRepo.Setup(x => x.GetStateStringAsync())
        .ReturnsAsync(new SuccessResult<string>(testState));

    var expectedScopes = string.Join("%20", ScopeExtensions.GetRequiredScopes());
    var expectedUrl = $"https://id.twitch.tv/oauth2/authorize?client_id={oAuthConfigSettings.ClientId}" +
        $"&redirect_uri={oAuthConfigSettings.RedirectUri}&response_type=code&scope={expectedScopes}" +
        $"&state={testState}";

    // Act
    var result = await handler.HandleAsync(query);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(expectedUrl, result.Value);
    mockStateStringRepo.Verify(x => x.GetStateStringAsync(), Times.Once);
  }

  [Fact]
  public async Task HandleAsync_WhenStateRepoFails_ReturnsErrorAsync()
  {
    // Arrange
    var query = new GetTwitchUrlQuery();
    var error = new ValidationError("Failed to get state string");
    _ = mockStateStringRepo.Setup(x => x.GetStateStringAsync())
        .ReturnsAsync(new FailureResult<string>(error));

    // Act
    var result = await handler.HandleAsync(query);

    // Assert
    Assert.True(result.IsError);
    Assert.Equal(error, result.Error);
    mockStateStringRepo.Verify(x => x.GetStateStringAsync(), Times.Once);
  }
}
