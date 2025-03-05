namespace Codeacula.API.Infrastructure.JWT;

using System.IdentityModel.Tokens.Jwt;
using Codeacula.Core.Common.Results;
using Codeacula.Core.Domain.Authentication.Models;
using Codeacula.Core.Domain.Users.Models;

public interface IJwtService
{
  OperationResult<JwtSecurityToken> Convert(string token);

  OperationResult<string> GenerateRefreshToken(User user, string refreshToken);

  OperationResult<TokenPair> GetTokens(User user, RefreshToken refreshToken);

  OperationResult<Guid> GetUserId(JwtSecurityToken token);

  Task<OperationResult<bool>> ValidateTokenAsync(string token);

  Task<OperationResult<bool>> ValidateRefreshTokenAsync(string token);
}
