using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Adapters;

public interface IItunesAdapter
{
    Task<AppleGenre[]> GetGenres();
}