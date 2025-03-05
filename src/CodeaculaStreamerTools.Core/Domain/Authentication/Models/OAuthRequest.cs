namespace CodeaculaStreamerTools.Core.Domain.Authentication.Models;

/// <summary>
/// Generic request class, since most OAuth workflows should follow this format.
/// </summary>
public record OAuthRequest
{
  public required string Code { get; init; }

  public required string State { get; init; }
}
