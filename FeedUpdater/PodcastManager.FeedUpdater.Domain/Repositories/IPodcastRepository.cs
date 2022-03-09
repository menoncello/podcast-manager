using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Domain.Repositories;

public interface IPodcastRepository
{
    Task<UpdatePodcast[]> ListPodcastToUpdate();
    Task<UpdatePodcast[]> ListPublishedPodcastToUpdate();
}