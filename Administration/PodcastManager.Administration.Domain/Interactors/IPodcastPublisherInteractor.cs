namespace PodcastManager.Administration.Domain.Interactors;

public interface IPodcastPublisherInteractor
{
    Task PublishAllFromPlaylists();
}