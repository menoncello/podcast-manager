using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.FeedUpdater.Adapters;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.CrossCutting.Rabbit;

public class RabbitUpdaterEnqueuerAdapter : BaseRabbitEnqueuerAdapter,
    IUpdaterEnqueuerAdapter
{
    public void EnqueueUpdateAllPodcasts() =>
        Publish(UpdaterRabbitConfiguration.UpdatePodcastsQueue, new UpdatePodcasts());

    public void EnqueueUpdateAllPublishedPodcasts() =>
        Publish(UpdaterRabbitConfiguration.UpdatePublishedPodcastsQueue, 
            new UpdatePublishedPodcasts());

    public void EnqueueUpdatePodcast(UpdatePodcast podcast) =>
        Publish(UpdaterRabbitConfiguration.UpdatePodcastQueue, podcast);

    public void EnqueueUpdatePodcasts(IReadOnlyCollection<UpdatePodcast> podcasts) =>
        BatchPublish(UpdaterRabbitConfiguration.UpdatePodcastQueue, podcasts);
}