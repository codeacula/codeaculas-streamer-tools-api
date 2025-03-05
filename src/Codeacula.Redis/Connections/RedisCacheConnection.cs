namespace Codeacula.Redis.Connections;

using System.Diagnostics.CodeAnalysis;
using StackExchange.Redis;

public class RedisCacheConnection
{
  private readonly IConnectionMultiplexer multiplexer;

  [ExcludeFromCodeCoverage]
  public RedisCacheConnection(string connectionString)
  {
    multiplexer = ConnectionMultiplexer.Connect(connectionString);
  }

  public RedisCacheConnection(IConnectionMultiplexer multiplexer)
  {
    this.multiplexer = multiplexer;
  }

  public void Disconnect() => multiplexer.Close();

  public IDatabase GetDatabase() => multiplexer.GetDatabase();
}
