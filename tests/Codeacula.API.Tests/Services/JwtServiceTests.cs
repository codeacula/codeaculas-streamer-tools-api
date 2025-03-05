namespace Codeacula.API.Tests.Services;

using System.IdentityModel.Tokens.Jwt;
using Codeacula.API.Infrastructure.JWT;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Core.Domain.Authentication.Models;
using Codeacula.Core.Domain.Users.Models;

public class JwtServiceTests
{
  private readonly JwtService service;
  private readonly string audience = "test_audience";
  private readonly string issuer = "test_issuer";
  private readonly User testUser;

  public JwtServiceTests()
  {
    var configSettings = new JwtConfigSettings()
    {
      Audience = audience,
      Issuer = issuer,
      Key = "test_signing_key_that_is_long_enough",
      RefreshKey = "test_refresh_key_that_is_long_enough",
    };

    service = new JwtService(configSettings);

    testUser = new User
    {
      DisplayName = "Test User",
      Id = Guid.NewGuid(),
      Email = "test@example.com",
      Username = "testuser",
      ProfileImageUrl = "https://example.com/profile.jpg",
      Roles = [new() { Name = "User" }, new() { Name = "Admin" }],
    };
  }

  [Fact]
  public void GenerateToken_WithValidUser_ReturnsValidToken()
  {
    // Act
    var result = service.GenerateToken(testUser);

    // Assert
    Assert.True(result.IsSuccess);
    var token = new JwtSecurityTokenHandler().ReadJwtToken(result.Value);
    Assert.Equal(testUser.Username, token.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value);
    Assert.Equal(testUser.Email, token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
    Assert.Equal(testUser.Id.ToString(), token.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
    Assert.Collection(
      token.Claims.Where(c => c.Type == "role"),
      claim => Assert.Equal("User", claim.Value),
      claim => Assert.Equal("Admin", claim.Value));
  }

  [Fact]
  public void GenerateRefreshToken_WithValidInput_ReturnsValidToken()
  {
    // Arrange
    var refreshTokenString = Guid.NewGuid().ToString();

    // Act
    var result = service.GenerateRefreshToken(testUser, refreshTokenString);

    // Assert
    Assert.True(result.IsSuccess);
    var token = new JwtSecurityTokenHandler().ReadJwtToken(result);
    Assert.Equal(testUser.Username, token.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value);
    Assert.Equal(testUser.Email, token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
    Assert.Equal(testUser.Id.ToString(), token.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
    Assert.Equal(refreshTokenString, token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
  }

  [Fact]
  public void GetTokens_WithValidInput_ReturnsValidTokenPair()
  {
    // Arrange
    var refreshToken = new RefreshToken();

    // Act
    var result = service.GetTokens(testUser, refreshToken);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value.AccessToken);
    Assert.NotNull(result.Value.RefreshToken);
  }

  [Fact]
  public void Convert_WithValidToken_ReturnsToken()
  {
    // Arrange
    var tokenResult = service.GenerateToken(testUser);
    Assert.True(tokenResult.IsSuccess);

    // Act
    var result = service.Convert(tokenResult.Value);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(testUser.Username, result.Value.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value);
  }

  [Fact]
  public void Convert_WithInvalidToken_ReturnsError()
  {
    // Act
    var result = service.Convert("invalid_token");

    // Assert
    Assert.True(result.IsError);
  }

  [Fact]
  public async Task ValidateToken_WithValidToken_ReturnsTrueAsync()
  {
    // Arrange
    var tokenResult = service.GenerateToken(testUser);
    Assert.True(tokenResult.IsSuccess);

    // Act
    var result = await service.ValidateTokenAsync(tokenResult.Value);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.True(result.Value);
  }

  [Fact]
  public async Task ValidateToken_WithInvalidToken_ReturnsFalseAsync()
  {
    // Act
    var result = await service.ValidateTokenAsync("invalid_token");

    // Assert
    Assert.True(result.IsSuccess);
    Assert.False(result.Value);
  }

  [Fact]
  public async Task ValidateRefreshToken_WithValidToken_ReturnsTrueAsync()
  {
    // Arrange
    var tokenResult = service.GenerateRefreshToken(testUser, Guid.NewGuid().ToString());
    Assert.True(tokenResult.IsSuccess);

    // Act
    var result = await service.ValidateRefreshTokenAsync(tokenResult.Value);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.True(result.Value);
  }

  [Fact]
  public void GetUserId_WithValidToken_ReturnsUserId()
  {
    // Arrange
    var tokenResult = service.GenerateToken(testUser);
    Assert.True(tokenResult.IsSuccess);
    var convertResult = service.Convert(tokenResult.Value);
    Assert.True(convertResult.IsSuccess);

    // Act
    var result = service.GetUserId(convertResult.Value);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal(testUser.Id, result.Value);
  }
}
