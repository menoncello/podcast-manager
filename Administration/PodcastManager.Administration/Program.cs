using PodcastManager.Administration.CrossCutting.IoC;
using PodcastManager.Administration.CrossCutting.Rabbit;
using PodcastManager.CrossCutting.Rabbit;
using RabbitMQ.Client;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ApplicationName", "Administration")
    .CreateLogger();


Log.Logger.Information("iTunes Crawler service starting");

var closing = new AutoResetEvent(false);

var repositoryFactory = new RepositoryFactory();
var connectionFactory = new ConnectionFactory { HostName = BaseRabbitConfiguration.Host };

var interactorFactory = new InteractorFactory();
interactorFactory.SetRepositoryFactory(repositoryFactory);
interactorFactory.SetLogger(Log.Logger);

var listener = new RabbitAdministrationListenerAdapter();
listener.SetInteractorFactory(interactorFactory);
listener.SetConnectionFactory(connectionFactory);
listener.SetLogger(Log.Logger);
listener.Listen();

Console.CancelKeyPress += OnExit;
closing.WaitOne();
listener.Dispose();

void OnExit(object? sender, ConsoleCancelEventArgs args)
{
    Log.Logger.Information("Service Administration is exiting");
    closing.Set();
}