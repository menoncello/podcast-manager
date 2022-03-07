using PodcastManager.ItunesCrawler.CrossCutting.IoC;

Console.WriteLine($"{DateTime.Now} - iTunes Crawler service starting");

var closing = new AutoResetEvent(false);

var repositoryFactory = new RepositoryFactory();
var interactorFactory = new InteractorFactory(repositoryFactory);

var listener = new BusListener(interactorFactory);
listener.Listen();


Console.CancelKeyPress += OnExit;
closing.WaitOne();
listener.Dispose();

void OnExit(object? sender, ConsoleCancelEventArgs args)
{
    Console.WriteLine($"{DateTime.Now} - Exit");
    closing.Set();
}
