namespace CodeaculaStreamerTools.Core.Domain.Authentication.Handlers;

using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;
using CodeaculaStreamerTools.Core.Domain.Authentication.Queries;
using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;
using CodeaculaStreamerTools.Core.Domain.Twitch.Helpers;

public class GetTwitchUrlHandler(IStateStringRepo stateStringRepo, OAuthConfigSettings oAuthConfigSettings) : IQueryHandler<GetTwitchUrlQuery, string>
{
  private readonly OAuthConfigSettings oAuthConfigSettings = oAuthConfigSettings;
  private readonly IStateStringRepo stateStringRepo = stateStringRepo;

  public async Task<OperationResult<string>> HandleAsync(GetTwitchUrlQuery query)
  {
    var state = await stateStringRepo.GetStateStringAsync();

    if (state.IsError)
    {
      return state;
    }

    return $"https://id.twitch.tv/oauth2/authorize?client_id={oAuthConfigSettings.ClientId}&redirect_uri={oAuthConfigSettings.RedirectUri}&response_type=code&scope={ScopeExtensions.GetRequiredScopes()}&state={state.Value}";
  }
}
