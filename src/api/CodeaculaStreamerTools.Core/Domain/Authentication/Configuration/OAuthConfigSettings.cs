namespace CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;

using CodeaculaStreamerTools.Core.Domain.Authentication.DTOs;

public record OAuthConfigSettings
{
  public required string ClientId { get; init; }

  public required string ClientSecret { get; init; }

  public string GrantType { get; init; } = "authorization_code";

  public required string RedirectUri { get; init; }

  public ExternalOAuthRequest ToExternalOAuthRequest(string code)
  {
    return new ExternalOAuthRequest
    {
      ClientId = ClientId,
      ClientSecret = ClientSecret,
      Code = code,
      GrantType = GrantType,
      RedirectUri = RedirectUri,
    };
  }
}
