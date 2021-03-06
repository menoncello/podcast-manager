using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.ItunesCrawler.Domain.Factories;
using PodcastManager.ItunesCrawler.Messages;
using RabbitMQ.Client;

namespace PodcastManager.ItunesCrawler.CrossCutting.Rabbit;

public class RabbitItunesCrawlerListenerAdapter : BaseRabbitListenerAdapter
{
    private IInteractorFactory interactorFactory = null!;

    public void SetInteractorFactory(IInteractorFactory interactorFactory) =>
        this.interactorFactory = interactorFactory;

    public override void Listen()
    {
        ListenTo<ImportAll>(Rabbit.ItunesCrawlerRabbitConfiguration.ImportAllQueue,
            _ => interactorFactory.CreateGenre().Execute(), 2, false);
        ListenTo<Letter>(Rabbit.ItunesCrawlerRabbitConfiguration.ImportLetterQueue, 
            interactorFactory.CreateLetter().Execute, 2, false);
        ListenTo<Page>(Rabbit.ItunesCrawlerRabbitConfiguration.ImportPageQueue,
            interactorFactory.CreatePage().Execute, 3, false);
    }
}