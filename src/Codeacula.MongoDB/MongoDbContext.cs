namespace Codeacula.MongoDB;

using global::MongoDB.Driver;

public class MongoDbContext(IMongoDatabase mongoDatabase) : IMongoDbContext
{
}
