namespace PodcastManager.Feed.Domain.Repositories;

public interface IPlaylistRepository
{
    Task<Models.Feed> GetFeed(string username, string slug);
}