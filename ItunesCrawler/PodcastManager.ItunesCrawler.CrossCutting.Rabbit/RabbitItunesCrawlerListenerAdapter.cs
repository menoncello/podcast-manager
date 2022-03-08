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
        ListenTo<ImportAll>(Rabbit.RabbitConfiguration.ImportAllQueue,
            _ => interactorFactory.CreateGenre().Execute());
        ListenTo<Letter>(Rabbit.RabbitConfiguration.ImportLetterQueue, 
            interactorFactory.CreateLetter().Execute);
        ListenTo<Page>(Rabbit.RabbitConfiguration.ImportPageQueue,
            interactorFactory.CreatePage().Execute);
    }
}