namespace Codeacula.Core.Domain.Twitch.Services;

using Codeacula.Core.Common.Results;
using Codeacula.Core.Domain.Authentication.Models;
using Codeacula.Core.Domain.Twitch.DTOs;

public interface ITwitchService
{
  Task<OperationResult<OAuthResponse>> AuthorizeAsync(string code);

  Task<OperationResult<TwitchUserInfoDTO>> GetCurrentUserAsync();

  void SetTokens(string accessToken, string refreshToken);
}
