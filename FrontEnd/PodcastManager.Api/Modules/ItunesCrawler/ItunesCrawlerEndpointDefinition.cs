using Microsoft.AspNetCore.Mvc;
using PodcastManager.Api.Definitions;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.CrossCutting.Rabbit;
using RabbitMQ.Client;

namespace PodcastManager.Api.Modules.ItunesCrawler;

public class ItunesCrawlerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/import/start", ImportStartHandler);
    }

    internal static void ImportStartHandler([FromServices] IItunesCrawlerEnqueuerAdapter enqueuer) =>
        enqueuer.EnqueueStart();

    public void DefineServices(IServiceCollection services)
    {
        services
            .AddSingleton<IItunesCrawlerEnqueuerAdapter>(s =>
            {
                var adapter = new RabbitItunesCrawlerEnqueuerAdapter();
                var factory = s.GetService<IConnectionFactory>()!;
                adapter.SetConnection(factory.CreateConnection());
                return adapter;
            });
    }
}