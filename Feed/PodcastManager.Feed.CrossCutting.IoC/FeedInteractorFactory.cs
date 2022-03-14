using PodcastManager.Feed.Application.Adapters;
using PodcastManager.Feed.Application.Services;
using PodcastManager.Feed.Domain.Factories;
using PodcastManager.Feed.Domain.Interactors;

namespace PodcastManager.Feed.CrossCutting.IoC;

public class FeedInteractorFactory : IFeedInteractorFactory
{
    private IFeedRepositoryFactory repositoryFactory = null!;

    public IPlaylistInteractor CreatePlaylist()
    {
        var service = new PlaylistService();
        service.SetFeed(new FeedAdapter());
        service.SetRepository(repositoryFactory.CreatePlaylist());
        return service;
    }

    public void SetRepositoryFactory(IFeedRepositoryFactory repositoryFactory) =>
        this.repositoryFactory = repositoryFactory;
}