using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Adapters;

public interface IItunesCrawlerEnqueuerAdapter
{
    void EnqueueLetter(Letter letter);
    void EnqueuePage(Page page);
    void EnqueueStart();
}