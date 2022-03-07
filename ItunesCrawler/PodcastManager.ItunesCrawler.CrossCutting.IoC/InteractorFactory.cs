using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Application.Services;
using PodcastManager.ItunesCrawler.CrossCutting.Http;
using PodcastManager.ItunesCrawler.CrossCutting.Rabbit;
using PodcastManager.ItunesCrawler.Domain.Factories;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using RabbitMQ.Client;

namespace PodcastManager.ItunesCrawler.CrossCutting.IoC;

public class InteractorFactory : IInteractorFactory
{
    private readonly IRepositoryFactory repositoryFactory;
    private readonly IConnectionFactory rabbitConnectionFactory;

    public InteractorFactory(IRepositoryFactory repositoryFactory)
    {
        this.repositoryFactory = repositoryFactory;
        rabbitConnectionFactory = new ConnectionFactory
        {
            HostName = Rabbit.Configuration.Host
        };
    }
    
    public IGenreInteractor CreateGenre()
    {
        var service = new GenreService();
        
        service.SetEnqueuer(CreateEnqueuerAdapter());
        service.SetItunes(CreateItunesAdapter());
        
        return service;
    }

    public ILetterInteractor CreateLetter()
    {
        var service = new LetterService();
        service.SetEnqueuer(CreateEnqueuerAdapter());
        service.SetItunes(CreateItunesAdapter());
        return service;
    }

    public IPageInteractor CreatePage()
    {
        var service = new PageService();
        service.SetItunes(CreateItunesAdapter());
        service.SetRepository(repositoryFactory.CreatePodcast());
        return service;
    }
    
    private IItunesAdapter CreateItunesAdapter()
    {
        var adapter = new ItunesAdapter();
        adapter.SetFactory(new HttpClientFactory());
        return adapter;
    }

    private IEnqueuerAdapter CreateEnqueuerAdapter()
    {
        var adapter = new RabbitEnqueuerAdapter();
        adapter.SetConnection(rabbitConnectionFactory.CreateConnection());
        return adapter;
    }
}