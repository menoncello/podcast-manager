using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.FeedUpdater.Domain.Repositories;

namespace PodcastManager.FeedUpdater.CrossCutting.Mongo;

public class MongoEpisodeRepository : MongoRepository, IEpisodeRepository
{
    
}