namespace CodeaculaStreamerTools.Core;

using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Common.Services;
using CodeaculaStreamerTools.Core.Domain.Authentication.Commands;
using CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;
using CodeaculaStreamerTools.Core.Domain.Authentication.Handlers;
using CodeaculaStreamerTools.Core.Domain.Authentication.Queries;
using CodeaculaStreamerTools.Core.Domain.Authentication.Results;
using CodeaculaStreamerTools.Core.Domain.Site.Handlers;
using CodeaculaStreamerTools.Core.Domain.Site.Models;
using CodeaculaStreamerTools.Core.Domain.Site.Queries;
using CodeaculaStreamerTools.Core.Domain.Twitch.Services;
using CodeaculaStreamerTools.Core.Services;
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
