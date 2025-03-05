namespace CodeaculaStreamerTools.Core.Domain.Authentication.Repos;

using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Models;
using CodeaculaStreamerTools.Core.Domain.Users.Models;

public interface IRefreshTokenRepo
{
  Task<OperationResult<RefreshToken>> GenerateRefreshTokenAsync(User user);

  /// <summary>
  /// Marks the provided token as invalid and returns a new one if the original was valid.
  /// </summary>
  /// <param name="token">The refresh token to use to generate a new one.</param>
  /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
  Task<OperationResult<RefreshToken>> RegenerateTokenAsync(string token);
}
