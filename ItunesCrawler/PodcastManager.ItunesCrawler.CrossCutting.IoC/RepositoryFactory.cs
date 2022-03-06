using MongoDB.Driver;
using PodcastManager.ItunesCrawler.CrossCutting.Mongo;
using PodcastManager.ItunesCrawler.Domain.Factories;
using PodcastManager.ItunesCrawler.Domain.Repositories;

namespace PodcastManager.ItunesCrawler.CrossCutting.IoC;

public class RepositoryFactory : IRepositoryFactory
{
    public IPodcastRepository CreatePodcast()
    {
        var client = new MongoClient(Mongo.Configuration.MongoUrl);
        var database = client.GetDatabase(Mongo.Configuration.MongoDatabase);
        var repository = new MongoPodcastRepository();
        
        repository.SetDatabase(database);
        
        return repository;
    }
}