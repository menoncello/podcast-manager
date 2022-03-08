using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.CrossCutting.Rabbit;

public class RabbitEnqueuerAdapter : BaseRabbitEnqueuerAdapter, IEnqueuerAdapter
{
    public void EnqueueLetter(Letter letter) =>
        BasicPublish(RabbitConfiguration.ImportLetterQueue, letter);

    public void EnqueuePage(Page page) =>
        BasicPublish(RabbitConfiguration.ImportPageQueue, page);
}