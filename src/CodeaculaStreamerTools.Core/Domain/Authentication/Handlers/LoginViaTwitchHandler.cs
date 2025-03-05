namespace CodeaculaStreamerTools.Core.Domain.Authentication.Handlers;

using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Commands;
using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;
using CodeaculaStreamerTools.Core.Domain.Authentication.Results;
using CodeaculaStreamerTools.Core.Domain.Twitch.Services;

internal sealed class LoginViaTwitchHandler(
  IStateStringRepo stateStringRepo,
  ITwitchService twitchService) : ICommandHandler<LoginViaTwitchCommand, LoginViaTwitchResult>
{
  public async Task<OperationResult<LoginViaTwitchResult>> HandleAsync(LoginViaTwitchCommand command)
  {
    var validationResult = await stateStringRepo.ValidateStateStringAsync(command.State);

    if (validationResult.IsError)
    {
      return validationResult.Error!;
    }

    var authorizationResult = await twitchService.AuthorizeAsync(command.Code);

    if (authorizationResult.IsError)
    {
      return authorizationResult.Error!;
    }

    // Use the tokens to fetch the user from twitch
    twitchService.SetTokens(authorizationResult.Value.AccessToken, authorizationResult.Value.RefreshToken);
    var twitchUserInfoResult = await twitchService.GetCurrentUserAsync();

    if (twitchUserInfoResult.IsError)
    {
      return twitchUserInfoResult.Error!;
    }

    // Look up the local user from the twitch info, or create a new user

    // Generate a new system access token/refresh token pair
    // Set the refresh token JWT in a cookie
    // Return the system access JWT
    return new LoginViaTwitchResult
    {
      AccessToken = authorizationResult.Value.AccessToken,
      RefreshToken = authorizationResult.Value.RefreshToken,
      UserId = Guid.NewGuid(),
    };
  }

  public Task<OperationResult<bool>> ValidateAsync(LoginViaTwitchCommand command) => Task.FromResult(OperationResult.Ok(value: true));
}
