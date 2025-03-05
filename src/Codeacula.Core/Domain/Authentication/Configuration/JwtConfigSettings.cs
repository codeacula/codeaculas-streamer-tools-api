namespace Codeacula.Core.Domain.Authentication.Configuration;

using System.Text;
using Microsoft.IdentityModel.Tokens;

public record JwtConfigSettings
{
  public required string Audience { get; init; }

  public required string Issuer { get; init; }

  public required string Key { get; init; }

  public required string RefreshKey { get; init; }

  public SymmetricSecurityKey SigningKey => new(Encoding.UTF8.GetBytes(Key));

  public SymmetricSecurityKey RefreshSigningKey => new(Encoding.UTF8.GetBytes(RefreshKey));
}
