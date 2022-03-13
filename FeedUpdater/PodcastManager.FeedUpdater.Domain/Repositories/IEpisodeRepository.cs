using PodcastManager.FeedUpdater.Domain.Models;

namespace PodcastManager.FeedUpdater.Domain.Repositories;

public interface IEpisodeRepository
{
    Task<(int, int)> Save(int code, Item[] feedItems);
    Task<int> EpisodeCount(int code);
}