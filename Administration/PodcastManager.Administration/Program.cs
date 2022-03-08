using PodcastManager.Administration.CrossCutting.IoC;
using PodcastManager.Administration.CrossCutting.Rabbit;
using PodcastManager.CrossCutting.Rabbit;
using RabbitMQ.Client;

Console.WriteLine($"{DateTime.Now} - iTunes Crawler service starting");

var closing = new AutoResetEvent(false);

var repositoryFactory = new RepositoryFactory();
var connectionFactory = new ConnectionFactory { HostName = BaseRabbitConfiguration.Host };

var interactorFactory = new InteractorFactory();
interactorFactory.SetRepositoryFactory(repositoryFactory);

var listener = new RabbitAdministrationListenerAdapter();
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