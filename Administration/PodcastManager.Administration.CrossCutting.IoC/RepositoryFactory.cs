using MongoDB.Driver;
using PodcastManager.Administration.CrossCutting.Mongo;
using PodcastManager.Administration.Domain.Factories;
using PodcastManager.Administration.Domain.Repositories;
using PodcastManager.Core.CrossCutting.Mongo;

namespace PodcastManager.Administration.CrossCutting.IoC;

public class RepositoryFactory : IRepositoryFactory
{
    public IPodcastRepository CreatePodcast()
    {
        var client = new MongoClient(MongoConfiguration.MongoUrl);
        var database = client.GetDatabase(MongoConfiguration.MongoDatabase);
        var repository = new MongoPodcastRepository();
        
        repository.SetDatabase(database);
        
        return repository;
    }

    public IPlaylistRepository CreatePlaylist()
    {
        var client = new MongoClient(MongoConfiguration.MongoUrl);
        var database = client.GetDatabase(MongoConfiguration.MongoDatabase);
        var repository = new MongoPlaylistRepository();
        
        repository.SetDatabase(database);
        
        return repository;
    }
}