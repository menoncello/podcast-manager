using PodcastManager.Feed.Domain.Interactors;

namespace PodcastManager.Feed.Domain.Factories;

public interface IFeedInteractorFactory
{
    IPlaylistInteractor CreatePlaylist();
}