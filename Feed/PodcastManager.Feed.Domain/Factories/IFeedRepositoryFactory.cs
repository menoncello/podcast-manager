using PodcastManager.Feed.Domain.Repositories;

namespace PodcastManager.Feed.Domain.Factories;

public interface IFeedRepositoryFactory
{
    IPlaylistRepository CreatePlaylist();
}