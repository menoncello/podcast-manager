using MongoDB.Driver;
using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.Feed.CrossCutting.Mongo;
using PodcastManager.Feed.Domain.Factories;
using PodcastManager.Feed.Domain.Repositories;

namespace PodcastManager.Feed.CrossCutting.IoC;

public class FeedRepositoryFactory : IFeedRepositoryFactory
{
    public IPlaylistRepository CreatePlaylist()
    {
        var repository = new MongoPlaylistRepository();
        repository.SetDatabase(GetDatabase());
        return repository;
    }
    
    private static IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(MongoConfiguration.MongoUrl);
        var database = client.GetDatabase(MongoConfiguration.MongoDatabase);
        return database;
    }
}