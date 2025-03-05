namespace Codeacula.Core.Domain.Authentication.Models;

/// <summary>
/// The result of an OAuth request from an OAuth provider.
/// </summary>
public record OAuthResponse
{
  public required string AccessToken { get; init; }

  public required DateTime ExpiresOn { get; init; }

  public required string RefreshToken { get; init; }
}
