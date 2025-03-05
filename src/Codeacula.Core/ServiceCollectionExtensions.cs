namespace Codeacula.Core;

using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Common.Services;
using Codeacula.Core.Domain.Authentication.Commands;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Core.Domain.Authentication.Handlers;
using Codeacula.Core.Domain.Authentication.Queries;
using Codeacula.Core.Domain.Authentication.Results;
using Codeacula.Core.Domain.Site.Handlers;
using Codeacula.Core.Domain.Site.Models;
using Codeacula.Core.Domain.Site.Queries;
using Codeacula.Core.Domain.Twitch.Services;
using Codeacula.Core.Services;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddCoreContext(this IServiceCollection services, OAuthConfigSettings oAuthConfigSettings)
  {
    _ = services.AddHttpClient();
    _ = services.AddSingleton(oAuthConfigSettings)
      .AddTransient<IMediatorService, MediatorService>()
      .AddScoped<ITwitchService, TwitchService>();

    return services;
  }

  public static IServiceCollection AddDomainContext(this IServiceCollection services)
  {
    _ = services
      .AddScoped<ICommandHandler<LoginViaTwitchCommand, LoginViaTwitchResult>, LoginViaTwitchHandler>();

    _ = services
      .AddScoped<IQueryHandler<GetTwitchUrlQuery, string>, GetTwitchUrlHandler>()
      .AddScoped<IQueryHandler<SiteStatusQuery, SiteStatus>, SiteStatusQueryHandler>();

    return services;
  }
}
