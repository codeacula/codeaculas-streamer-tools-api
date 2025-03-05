namespace CodeaculaStreamerTools.Core.Domain.Twitch.Services;

using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Models;
using CodeaculaStreamerTools.Core.Domain.Twitch.DTOs;

public interface ITwitchService
{
  Task<OperationResult<OAuthResponse>> AuthorizeAsync(string code);

  Task<OperationResult<TwitchUserInfoDTO>> GetCurrentUserAsync();

  void SetTokens(string accessToken, string refreshToken);
}
