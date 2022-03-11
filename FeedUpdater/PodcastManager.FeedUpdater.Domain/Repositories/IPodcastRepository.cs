using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Domain.Repositories;

public interface IPodcastRepository
{
    Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate();
    Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate();
}