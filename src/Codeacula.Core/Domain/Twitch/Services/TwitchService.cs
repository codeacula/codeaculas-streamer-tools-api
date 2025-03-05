namespace Codeacula.Core.Domain.Twitch.Services;

using System.Text.Json;
using Codeacula.Core.Common.Results;
using Codeacula.Core.Common.Services;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Core.Domain.Authentication.DTOs;
using Codeacula.Core.Domain.Authentication.Models;
using Codeacula.Core.Domain.Twitch.DTOs;
using Codeacula.Core.Domain.Twitch.Errors;

public class TwitchService : ITwitchService
{
  private string? accessToken;
  private readonly IHttpClientWrapper httpClientWrapper;
  private string? refreshToken;

  private readonly OAuthConfigSettings twitchConfigSettings;

  public TwitchService(IHttpClientWrapper httpClientWrapper, OAuthConfigSettings twitchConfigSettings)
  {
    this.httpClientWrapper = httpClientWrapper;
    this.twitchConfigSettings = twitchConfigSettings;

    this.httpClientWrapper.AddDefaultRequestHeader("Client-Id", twitchConfigSettings.ClientId);
  }

  public async Task<OperationResult<OAuthResponse>> AuthorizeAsync(string code)
  {
    var extOAuthReq = twitchConfigSettings.ToExternalOAuthRequest(code);

    var response = await httpClientWrapper.PostAsync(new Uri("https://id.twitch.tv/oauth2/token"), extOAuthReq.ToFormUrlEncodedContent());
    var responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
      return new HttpRequestFailedError($"Error: {response.StatusCode}, Details: {responseContent}");
    }

    var twitchAuthResponse = JsonSerializer.Deserialize<ExternalOAuthResponse>(responseContent);

    if (twitchAuthResponse == null)
    {
      return new NoValueReturnedError();
    }

    if (!string.IsNullOrEmpty(twitchAuthResponse.Error))
    {
      return new HttpRequestFailedError($"Error: {twitchAuthResponse.Error}");
    }

    if (twitchAuthResponse.AccessToken is null || twitchAuthResponse.RefreshToken is null)
    {
      return new NoValueReturnedError();
    }

    return new OAuthResponse
    {
      AccessToken = twitchAuthResponse.AccessToken,
      ExpiresOn = DateTime.UtcNow.AddSeconds(twitchAuthResponse.ExpiresIn ?? 0),
      RefreshToken = twitchAuthResponse.RefreshToken,
    };
  }

  public async Task<OperationResult<TwitchUserInfoDTO>> GetCurrentUserAsync()
  {
    var response = await httpClientWrapper.GetAsync(new Uri("https://api.twitch.tv/helix/users"));
    var twitchAuthResponse = await ValidateTwitchApiResponseAsync<TwitchUserInfoDTO>(response);

    if (!twitchAuthResponse.Data.Any())
    {
      return new TwitchNoUserData();
    }

    return twitchAuthResponse.Data.First();
  }

  public void SetTokens(string accessToken, string refreshToken)
  {
    this.accessToken = accessToken;
    this.refreshToken = refreshToken;

    httpClientWrapper.SetAuthorizationHeader("Bearer", accessToken);
  }

  private static async Task<TwitchApiResultDTO<T>> ValidateTwitchApiResponseAsync<T>(HttpResponseMessage message)
  {
    var content = await message.Content.ReadAsStringAsync();

    if (!message.IsSuccessStatusCode)
    {
      throw new HttpRequestException($"Error: {message.StatusCode}, Details: {content}");
    }

    return JsonSerializer.Deserialize<TwitchApiResultDTO<T>>(content) ?? throw new JsonException("Failed to deserialize response from Twitch");
  }
}
