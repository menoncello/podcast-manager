using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.CrossCutting.Rabbit;

public class RabbitItunesCrawlerEnqueuerAdapter : BaseRabbitEnqueuerAdapter, IItunesCrawlerEnqueuerAdapter
{
    public void EnqueueLetter(Letter letter) =>
        Publish(ItunesCrawlerRabbitConfiguration.ImportLetterQueue, letter);

    public void EnqueuePage(Page page) =>
        Publish(ItunesCrawlerRabbitConfiguration.ImportPageQueue, page);

    public void EnqueueStart() =>
        Publish(ItunesCrawlerRabbitConfiguration.ImportAllQueue, new ImportAll());
}