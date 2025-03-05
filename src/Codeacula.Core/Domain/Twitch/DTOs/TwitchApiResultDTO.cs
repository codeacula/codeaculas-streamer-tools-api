namespace Codeacula.Core.Domain.Twitch.DTOs;

using System.Text.Json.Serialization;

public record TwitchApiResultDTO<T>
{
  [JsonPropertyName("data")]
  public required IEnumerable<T> Data { get; init; }
}
