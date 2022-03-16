using PodcastManager.Administration;
using PodcastManager.Administration.CrossCutting.Rabbit;
using PodcastManager.Api.Definitions;
using PodcastManager.Feed.Adapters;
using PodcastManager.Feed.Application.Adapters;
using PodcastManager.Feed.CrossCutting.IoC;
using PodcastManager.Feed.Domain.Factories;
using PodcastManager.Feed.Domain.Interactors;
using RabbitMQ.Client;

namespace PodcastManager.Api.Modules.Feed;

public class FeedEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
    }

    public void DefineServices(IServiceCollection services)
    {
        services
            .AddSingleton<IPlaylistInteractor>(s =>
            {
                var factory = new FeedInteractorFactory();
                factory.SetRepositoryFactory(new FeedRepositoryFactory());
                return factory.CreatePlaylist();
            });
    }
}