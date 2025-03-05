namespace Codeacula.Redis;

using Codeacula.Core.Domain.Authentication.Repos;
using Codeacula.Redis.Connections;
using Codeacula.Redis.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddRedisContext(this IServiceCollection services, RedisCacheConnection redisCacheConnection)
  {
    _ = services.AddSingleton(redisCacheConnection)
      .AddSingleton<IStateStringRepo, StateStringRepo>();
    return services;
  }
}
