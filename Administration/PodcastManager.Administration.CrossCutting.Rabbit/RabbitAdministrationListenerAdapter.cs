using PodcastManager.Administration.Domain.Factories;
using PodcastManager.Administration.Messages;
using PodcastManager.CrossCutting.Rabbit;

namespace PodcastManager.Administration.CrossCutting.Rabbit;

public class RabbitAdministrationListenerAdapter : BaseRabbitListenerAdapter
{
    private IInteractorFactory interactorFactory = null!;

    public void SetInteractorFactory(IInteractorFactory interactorFactory) =>
        this.interactorFactory = interactorFactory;

    public override void Listen()
    {
        ListenTo<PublishAllFromPlaylists>(RabbitConfiguration.PublishAllFromPlaylistQueue,
            _ => interactorFactory.CreatePodcastPublisher().PublishAllFromPlaylists());
    }
}