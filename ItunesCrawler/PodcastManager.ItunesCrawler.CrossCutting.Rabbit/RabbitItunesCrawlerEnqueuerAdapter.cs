using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.CrossCutting.Rabbit;

public class RabbitItunesCrawlerEnqueuerAdapter : BaseRabbitEnqueuerAdapter, IItunesCrawlerEnqueuerAdapter
{
    public void EnqueueLetter(IEnumerable<Letter> letters) =>
        BatchPublish(ItunesCrawlerRabbitConfiguration.ImportLetterQueue, letters);

    public void EnqueuePage(IEnumerable<Page> pages) =>
        BatchPublish(ItunesCrawlerRabbitConfiguration.ImportPageQueue, pages);

    public void EnqueueStart() =>
        Publish(ItunesCrawlerRabbitConfiguration.ImportAllQueue, new ImportAll());
}