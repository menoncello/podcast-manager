using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Adapters;

public interface IUpdaterEnqueuerAdapter
{
    void EnqueueUpdateAllPodcasts();
    void EnqueueUpdateAllPublishedPodcasts();
    void EnqueueUpdatePodcast(UpdatePodcast podcast);
    void EnqueueUpdatePodcasts(IReadOnlyCollection<UpdatePodcast> podcasts);
}