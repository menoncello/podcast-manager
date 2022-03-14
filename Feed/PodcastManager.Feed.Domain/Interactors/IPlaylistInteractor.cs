namespace PodcastManager.Feed.Domain.Interactors;

public interface IPlaylistInteractor
{
    Task<string> FromPlaylist(string username, string slug);
}