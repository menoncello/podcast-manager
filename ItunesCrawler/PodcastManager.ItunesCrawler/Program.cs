using PodcastManager.ItunesCrawler.CrossCutting.IoC;
using PodcastManager.ItunesCrawler.CrossCutting.Rabbit;
using RabbitMQ.Client;
using RabbitConfiguration = PodcastManager.CrossCutting.Rabbit.Configuration;

Console.WriteLine($"{DateTime.Now} - iTunes Crawler service starting");

var closing = new AutoResetEvent(false);

var repositoryFactory = new RepositoryFactory();
var connectionFactory = new ConnectionFactory { HostName = RabbitConfiguration.Host };

var interactorFactory = new InteractorFactory();
interactorFactory.SetConnectionFactory(connectionFactory);
interactorFactory.SetRepositoryFactory(repositoryFactory);

var listener = new RabbitListenerAdapter();
listener.SetInteractorFactory(interactorFactory);
listener.SetConnectionFactory(connectionFactory);
listener.Listen();


Console.CancelKeyPress += OnExit;
closing.WaitOne();
listener.Dispose();

void OnExit(object? sender, ConsoleCancelEventArgs args)
{
    Console.WriteLine($"{DateTime.Now} - Exit");
    closing.Set();
}
