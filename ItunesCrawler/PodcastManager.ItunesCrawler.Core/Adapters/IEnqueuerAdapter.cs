using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Adapters;

public interface IEnqueuerAdapter
{
    void EnqueueLetter(Letter letter);
    void EnqueuePage(Page page);
}