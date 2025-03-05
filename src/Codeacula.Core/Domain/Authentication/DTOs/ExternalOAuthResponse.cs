namespace Codeacula.Core.Domain.Authentication.DTOs;

using System.Text.Json.Serialization;

public record ExternalOAuthResponse
{
  [JsonPropertyName("access_token")]
  public string? AccessToken { get; init; }

  [JsonPropertyName("error")]
  public string Error { get; init; } = string.Empty;

  [JsonPropertyName("expires_in")]
  public int? ExpiresIn { get; init; }

  [JsonPropertyName("message")]
  public int? Message { get; init; }

  [JsonPropertyName("refresh_token")]
  public string? RefreshToken { get; init; }

  [JsonPropertyName("scope")]
  public IEnumerable<string>? Scope { get; init; }

  [JsonPropertyName("status")]
  public int? Status { get; init; }

  [JsonPropertyName("token_type")]
  public string? TokenType { get; init; }
}
