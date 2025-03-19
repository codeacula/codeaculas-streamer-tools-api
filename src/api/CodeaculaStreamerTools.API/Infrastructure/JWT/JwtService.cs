namespace CodeaculaStreamerTools.API.Infrastructure.JWT;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeaculaStreamerTools.Core.Common.Errors;
using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;
using CodeaculaStreamerTools.Core.Domain.Authentication.Models;
using CodeaculaStreamerTools.Core.Domain.Twitch.Errors;
using CodeaculaStreamerTools.Core.Domain.Users.Models;
using Microsoft.IdentityModel.Tokens;

public sealed class JwtService(JwtConfigSettings jwtConfigSettings) : IJwtService
{
  private readonly string audience = jwtConfigSettings.Audience;
  private readonly string issuer = jwtConfigSettings.Issuer;
  private readonly SigningCredentials refreshSigningCredentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigSettings.RefreshKey)), SecurityAlgorithms.HmacSha256);
  private readonly SigningCredentials signingCredentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigSettings.Key)), SecurityAlgorithms.HmacSha256);
  private readonly JwtSecurityTokenHandler tokenHandler = new();

  public OperationResult<JwtSecurityToken> Convert(string token)
  {
    try
    {
      return tokenHandler.ReadJwtToken(token);
    }
    catch (SecurityTokenMalformedException)
    {
      return new JwtConversionError();
    }
  }

  public OperationResult<TokenPair> GetTokens(User user, RefreshToken refreshToken)
  {
    var newAccessToken = GenerateToken(user);

    if (newAccessToken.IsError)
    {
      return new TokenGenerationError(newAccessToken.ErrorMessage);
    }

    var newRefreshToken = GenerateRefreshToken(user, refreshToken.ToString());

    if (newAccessToken.IsError)
    {
      return new TokenGenerationError(newRefreshToken.ErrorMessage);
    }

    return new TokenPair(newAccessToken, newRefreshToken);
  }

  public OperationResult<string> GenerateRefreshToken(User user, string refreshToken)
  {
    var claims = new List<Claim>
      {
        new(ClaimTypes.Name, user.Username),
        new(ClaimTypes.Email, user.Email),
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(JwtRegisteredClaimNames.Jti, refreshToken),
      };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = refreshSigningCredentials,
      Issuer = issuer,
      Audience = audience,
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public OperationResult<string> GenerateToken(User user)
  {
    var claims = new List<Claim>
    {
      new(ClaimTypes.Name, user.Username),
      new(ClaimTypes.Email, user.Email),
      new(ClaimTypes.NameIdentifier, user.Id.ToString()),
    };

    foreach (var userRole in user.Roles)
    {
      claims.Add(new(ClaimTypes.Role, userRole.Name));
    }

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(15),
      SigningCredentials = signingCredentials,
      Issuer = issuer,
      Audience = audience,
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public OperationResult<Guid> GetUserId(JwtSecurityToken token)
  {
    var guid = token.Claims.FirstOrDefault(x => string.Equals(x.Type, JwtRegisteredClaimNames.NameId, StringComparison.Ordinal))?.Value;

    if (guid == null)
    {
      return new NoValueReturnedError();
    }

    return Guid.Parse(guid);
  }

  public Task<OperationResult<bool>> ValidateTokenAsync(string token) => ValidateJwtAsync(token, signingCredentials);

  public Task<OperationResult<bool>> ValidateRefreshTokenAsync(string token) => ValidateJwtAsync(token, refreshSigningCredentials);

  private async Task<OperationResult<bool>> ValidateJwtAsync(string token, SigningCredentials signingCredentials)
  {
    var validationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = issuer,
      ValidAudience = audience,
      IssuerSigningKey = signingCredentials.Key,
    });

    return validationResult.IsValid;
  }
}
