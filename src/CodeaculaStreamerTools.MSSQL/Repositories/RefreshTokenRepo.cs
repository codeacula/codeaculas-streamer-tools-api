namespace CodeaculaStreamerTools.MSSQL.Repositories;

using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Errors;
using CodeaculaStreamerTools.Core.Domain.Authentication.Models;
using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;
using CodeaculaStreamerTools.Core.Domain.Users.Models;
using CodeaculaStreamerTools.MSSQL.Entities;

public class RefreshTokenRepo(ISqlServerDBContext dbContext) : IRefreshTokenRepo
{
  private readonly ISqlServerDBContext dbContext = dbContext;

  public async Task<OperationResult<RefreshToken>> GenerateRefreshTokenAsync(User user)
  {
    RefreshTokenDBO refreshToken = new() { UserId = user.Id };
    _ = await dbContext.RefreshTokens.AddAsync(refreshToken);
    _ = await dbContext.SaveChangesAsync();

    return new RefreshToken();
  }

  public async Task<OperationResult<RefreshToken>> RegenerateTokenAsync(string token)
  {
    var refreshToken = await dbContext.RefreshTokens.FindAsync(Guid.Parse(token));

    if (refreshToken is null || refreshToken.ExpiresOn < DateTime.UtcNow)
    {
      if (refreshToken is not null)
      {
        _ = dbContext.RefreshTokens.Remove(refreshToken);
        _ = await dbContext.SaveChangesAsync();
      }

      return new InvalidTokenError();
    }

    var newToken = new RefreshTokenDBO { UserId = refreshToken.UserId };

    _ = await dbContext.RefreshTokens.AddAsync(newToken);
    _ = dbContext.RefreshTokens.Remove(refreshToken);
    _ = await dbContext.SaveChangesAsync();

    return new RefreshToken();
  }
}
