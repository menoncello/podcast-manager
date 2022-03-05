using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Adapters;

public interface IEnqueuerAdapter
{
    Task EnqueueLetter(Letter letter);
    Task EnqueuePage(Page page);
}