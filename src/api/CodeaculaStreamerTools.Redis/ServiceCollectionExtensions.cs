namespace CodeaculaStreamerTools.Redis;

using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;
using CodeaculaStreamerTools.Redis.Connections;
using CodeaculaStreamerTools.Redis.Repositories;
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
