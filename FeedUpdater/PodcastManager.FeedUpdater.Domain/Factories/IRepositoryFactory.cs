using PodcastManager.FeedUpdater.Domain.Repositories;

namespace PodcastManager.FeedUpdater.Domain.Factories;

public interface IRepositoryFactory
{
    Task<IEpisodeRepository> CreateEpisode();
    Task<IPodcastRepository> CreatePodcast();
}