using PodcastManager.Administration.Application.Services;
using PodcastManager.Administration.Domain.Factories;
using PodcastManager.Administration.Domain.Interactors;

namespace PodcastManager.Administration.CrossCutting.IoC;

public class InteractorFactory : IInteractorFactory
{
    private IRepositoryFactory repositoryFactory = null!;

    public void SetRepositoryFactory(IRepositoryFactory repositoryFactory) => 
        this.repositoryFactory = repositoryFactory;

    public IPodcastPublisherInteractor CreatePodcastPublisher()
    {
        var service = new PodcastPublisherService();
        service.SetPodcastRepository(repositoryFactory.CreatePodcast());
        service.SetPlaylistRepository(repositoryFactory.CreatePlaylist());
        return service;
    }
}