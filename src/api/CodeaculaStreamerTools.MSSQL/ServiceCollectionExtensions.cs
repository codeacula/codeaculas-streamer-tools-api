namespace CodeaculaStreamerTools.MSSQL;

using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;
using CodeaculaStreamerTools.MSSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddMSSQLContext(this IServiceCollection services, string connectionString)
  {
    _ = services.AddDbContext<SqlServerDBContext>(options =>
        options.UseSqlServer(connectionString));

    _ = services.AddScoped<ISqlServerDBContext, SqlServerDBContext>();
    _ = services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
    return services;
  }
}
