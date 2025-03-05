namespace CodeaculaStreamerTools.Infrastructure;

using CodeaculaStreamerTools.Core.Common.Services;
using CodeaculaStreamerTools.Core.Domain.Authentication.Configuration;
using CodeaculaStreamerTools.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructureContext(this IServiceCollection services, JwtConfigSettings jwtConfigSettings)
  {
    _ = services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
    _ = services.AddSingleton(jwtConfigSettings);

    return services;
  }
}
