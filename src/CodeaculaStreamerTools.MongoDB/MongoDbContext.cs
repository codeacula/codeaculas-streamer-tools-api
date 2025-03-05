namespace CodeaculaStreamerTools.MongoDB;

using global::MongoDB.Driver;

public class MongoDbContext(IMongoDatabase mongoDatabase) : IMongoDbContext
{
}
