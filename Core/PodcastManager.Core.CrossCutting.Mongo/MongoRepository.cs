using MongoDB.Driver;

namespace PodcastManager.Core.CrossCutting.Mongo;

public abstract class MongoRepository
{
    private IMongoDatabase database = null!;

    public void SetDatabase(IMongoDatabase database) =>
        this.database = database;

    protected IMongoCollection<T> GetCollection<T>(string name) =>
        database.GetCollection<T>(name);
}