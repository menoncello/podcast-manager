using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.FeedUpdater.Domain.Models;
using PodcastManager.FeedUpdater.Domain.Repositories;

namespace PodcastManager.FeedUpdater.CrossCutting.Mongo;

public class MongoEpisodeRepository : MongoRepository, IEpisodeRepository
{
    public Task<(int, int)> Save(int code, Item[] feedItems)
    {
        throw new NotImplementedException();
    }

    public Task<int> EpisodeCount(int code)
    {
        throw new NotImplementedException();
    }
}