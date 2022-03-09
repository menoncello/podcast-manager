using PodcastManager.Administration.Application.Services;
using PodcastManager.Administration.Domain.Factories;
using PodcastManager.Administration.Domain.Interactors;
using Serilog;

namespace PodcastManager.Administration.CrossCutting.IoC;

public class InteractorFactory : IInteractorFactory
{
    private IRepositoryFactory repositoryFactory = null!;
    private ILogger logger = null!;

    public void SetRepositoryFactory(IRepositoryFactory repositoryFactory) => 
        this.repositoryFactory = repositoryFactory;
    public void SetLogger(ILogger logger) => this.logger = logger;

    public IPodcastPublisherInteractor CreatePodcastPublisher()
    {
        var service = new PodcastPublisherService();
        service.SetPodcastRepository(repositoryFactory.CreatePodcast());
        service.SetPlaylistRepository(repositoryFactory.CreatePlaylist());
        service.SetLogger(logger);
        return service;
    }
}