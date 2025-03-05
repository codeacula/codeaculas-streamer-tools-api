namespace Codeacula.Core.Domain.Authentication.Results;

public record LoginViaTwitchResult
{
  public required string AccessToken { get; init; }

  public required string RefreshToken { get; init; }

  public Guid UserId { get; init; }
}
