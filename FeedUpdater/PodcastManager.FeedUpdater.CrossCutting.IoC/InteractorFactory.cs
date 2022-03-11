using PodcastManager.FeedUpdater.Adapters;
using PodcastManager.FeedUpdater.Application.Services;
using PodcastManager.FeedUpdater.CrossCutting.Rabbit;
using PodcastManager.FeedUpdater.Domain.Factories;
using PodcastManager.FeedUpdater.Domain.Interactors;
using RabbitMQ.Client;
using Serilog;

namespace PodcastManager.FeedUpdater.CrossCutting.IoC;

public class InteractorFactory : IInteractorFactory
{
    private ConnectionFactory connectionFactory = null!;
    private RepositoryFactory repositoryFactory = null!;
    private ILogger logger = null!;

    public void SetConnectionFactory(ConnectionFactory connectionFactory) =>
        this.connectionFactory = connectionFactory;
    public void SetRepositoryFactory(RepositoryFactory repositoryFactory) =>
        this.repositoryFactory = repositoryFactory;
    public void SetLogger(ILogger logger) => this.logger = logger;

    public IMultiplePodcastUpdaterInteractor CreateMultiple()
    {
        var service = new MultiplePodcastUpdaterService();
        service.SetEnqueuer(CreateEnqueuerAdapter());
        service.SetRepository(repositoryFactory.CreatePodcast());
        service.SetLogger(logger);
        return service;
    }

    public IPodcastUpdaterInteractor CreatePodcastUpdater()
    {
        var service = new PodcastUpdaterService();
        service.SetLogger(logger);
        return service;
    }
    
    private IUpdaterEnqueuerAdapter CreateEnqueuerAdapter()
    {
        var adapter = new RabbitUpdaterEnqueuerAdapter();
        adapter.SetConnection(connectionFactory.CreateConnection());
        return adapter;
    }

}