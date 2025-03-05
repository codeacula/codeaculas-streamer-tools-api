namespace Codeacula.MongoDB;

using global::MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfigurationSection mongodb)
  {
    _ = services.AddSingleton<IMongoClient>(_ =>
    {
      var connectionString = mongodb["ConnectionString"];
      return new MongoClient(connectionString);
    });

    _ = services.AddSingleton(sp =>
    {
      var client = sp.GetRequiredService<IMongoClient>();
      var dbname = mongodb["DatabaseName"];
      return client.GetDatabase(dbname);
    });
    _ = services.AddScoped<IMongoDbContext, MongoDbContext>();

    return services;
  }
}
