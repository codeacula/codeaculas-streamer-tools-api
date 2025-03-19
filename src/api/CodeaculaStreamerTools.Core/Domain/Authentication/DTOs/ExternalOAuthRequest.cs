namespace CodeaculaStreamerTools.Core.Domain.Authentication.DTOs;

using System.Text.Json.Serialization;

public record ExternalOAuthRequest
{
  [JsonPropertyName("client_id")]
  public required string ClientId { get; init; }

  [JsonPropertyName("client_secret")]
  public required string ClientSecret { get; init; }

  [JsonPropertyName("code")]
  public required string Code { get; init; }

  [JsonPropertyName("grant_type")]
  public required string GrantType { get; init; } = "authorization_code";

  [JsonPropertyName("redirect_uri")]
  public required string RedirectUri { get; init; }

  public FormUrlEncodedContent ToFormUrlEncodedContent()
  {
    var content = new Dictionary<string, string>(StringComparer.Ordinal)
    {
      { "client_id", ClientId },
      { "client_secret", ClientSecret },
      { "code", Code },
      { "grant_type", GrantType },
      { "redirect_uri", RedirectUri },
    };

    return new FormUrlEncodedContent(content);
  }
}
