namespace Codeacula.Infrastructure;

using Codeacula.Core.Common.Services;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Infrastructure.Http;
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
