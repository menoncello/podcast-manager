using PodcastManager.Administration.Domain.Repositories;

namespace PodcastManager.Administration.Domain.Factories;

public interface IRepositoryFactory
{
    IPodcastRepository CreatePodcast();
    IPlaylistRepository CreatePlaylist();
}