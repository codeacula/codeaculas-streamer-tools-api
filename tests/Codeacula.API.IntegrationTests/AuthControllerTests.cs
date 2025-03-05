namespace Codeacula.API.IntegrationTests;

using System.Net.Http.Json;
using Codeacula.API.Controllers;
using Codeacula.API.Infrastructure.JWT;
using Codeacula.API.Responses;
using Codeacula.Core.Common.Results;
using Codeacula.Core.Common.Services;
using Codeacula.Core.Domain.Authentication.Commands;
using Codeacula.Core.Domain.Authentication.DTOs;
using Codeacula.Core.Domain.Authentication.Queries;
using Codeacula.Core.Domain.Twitch.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;
  private readonly Mock<IJwtService> _jwtServiceMock;
  private readonly Mock<IMediatorService> _mediatorServiceMock;

  public AuthControllerTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
    _jwtServiceMock = new Mock<IJwtService>();
    _mediatorServiceMock = new Mock<IMediatorService>();
  }

  [Fact]
  public async Task GetTwitchUrlAsync_ReturnsUrl()
  {
    // Arrange
    var expectedUrl = "https://twitch.tv/auth";
    _mediatorServiceMock
      .Setup(m => m.ExecuteQueryAsync(It.IsAny<GetTitchUrlQuery>()))
      .ReturnsAsync(new SuccessResult<string>(expectedUrl));

    var client = _factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureServices(services =>
      {
        services.AddSingleton(_jwtServiceMock.Object);
        services.AddSingleton(_mediatorServiceMock.Object);
      });
    }).CreateClient();

    // Act
    var response = await client.GetAsync("/auth/twitch");

    // Assert
    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
    Assert.NotNull(result);
    Assert.Equal(expectedUrl, result?.Data);
  }

  [Fact]
  public async Task TwitchAuthAsync_ReturnsAccessToken()
  {
    // Arrange
    var request = new TwitchAuthRequestDTO { Code = "test_code", State = "test_state" };
    var expectedAccessToken = "access_token";
    var expectedRefreshToken = "refresh_token";

    _mediatorServiceMock
      .Setup(m => m.ExecuteCommandAsync(It.IsAny<LoginViaTwitchCommand>()))
      .ReturnsAsync(new SuccessResult<OAuthResponse>(new OAuthResponse
      {
        AccessToken = expectedAccessToken,
        RefreshToken = expectedRefreshToken
      }));

    var client = _factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureServices(services =>
      {
        services.AddSingleton(_jwtServiceMock.Object);
        services.AddSingleton(_mediatorServiceMock.Object);
      });
    }).CreateClient();

    // Act
    var response = await client.PostAsJsonAsync("/auth/twitch", request);

    // Assert
    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
    Assert.NotNull(result);
    Assert.Equal(expectedAccessToken, result?.Data);
  }

  [Fact]
  public async Task VerifyTokenAsync_ReturnsOk_WhenTokenIsValid()
  {
    // Arrange
    var token = "valid_token";
    var verifyTokenDTO = new VerifyTokenDTO { Token = token };

    _jwtServiceMock
      .Setup(j => j.ValidateTokenAsync(token))
      .ReturnsAsync(new SuccessResult<bool>(true));

    var client = _factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureServices(services =>
      {
        services.AddSingleton(_jwtServiceMock.Object);
        services.AddSingleton(_mediatorServiceMock.Object);
      });
    }).CreateClient();

    // Act
    var response = await client.GetAsync($"/auth/verify?Token={token}");

    // Assert
    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
    Assert.NotNull(result);
    Assert.True(result?.Data);
  }
}
