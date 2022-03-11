using PodcastManager.Administration.Messages;
using PodcastManager.CrossCutting.Rabbit;

namespace PodcastManager.Administration.CrossCutting.Rabbit;

public class RabbitAdministrationEnqueuerAdapter : BaseRabbitEnqueuerAdapter, IAdministrationEnqueuerAdapter
{
    public void EnqueuePublishPodcastFromPlaylists() =>
        BasicPublish(RabbitConfiguration.PublishAllFromPlaylistQueue, new PublishAllFromPlaylists());
}