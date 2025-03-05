namespace Codeacula.Core.Domain.Authentication.Handlers;

using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Common.Results;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Core.Domain.Authentication.Queries;
using Codeacula.Core.Domain.Authentication.Repos;
using Codeacula.Core.Domain.Twitch.Helpers;

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
