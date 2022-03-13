using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.FeedUpdater.Domain.Factories;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.CrossCutting.Rabbit;

public class RabbitUpdaterListenerAdapter : BaseRabbitListenerAdapter
{
    private IInteractorFactory interactorFactory = null!;

    public void SetInteractorFactory(IInteractorFactory interactorFactory) =>
        this.interactorFactory = interactorFactory;

    public override void Listen()
    {
        ListenTo<UpdatePodcast>(UpdaterRabbitConfiguration.UpdatePodcastQueue,
            podcast => interactorFactory.CreatePodcastUpdater().Execute(podcast));
        ListenTo<UpdatePodcasts>(UpdaterRabbitConfiguration.UpdatePodcastsQueue, 
            _ => interactorFactory.CreateMultiple().Execute());
        ListenTo<UpdatePublishedPodcasts>(UpdaterRabbitConfiguration.UpdatePublishedPodcastsQueue,
            _ => interactorFactory.CreateMultiple().ExecutePublished());
    }
}