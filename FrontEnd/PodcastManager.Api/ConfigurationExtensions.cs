using PodcastManager.Api.Definitions;
using PodcastManager.Api.Modules.Administration;
using PodcastManager.Api.Modules.ItunesCrawler;
using PodcastManager.CrossCutting.Rabbit;
using RabbitMQ.Client;

namespace PodcastManager.Api;

public static class ConfigurationExtensions
{
    public static IApplicationBuilder SetUp(this WebApplication app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        app.UseEndpointDefinitions();
        
        return app;
    }
    
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddBaseIoC()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddEndpointDefinitions(typeof(Program));
        
        return builder;
    }

    private static IServiceCollection AddBaseIoC(this IServiceCollection service)
    {
        return service
            .AddSingleton<IConnectionFactory>(_ => new ConnectionFactory {HostName = BaseRabbitConfiguration.Host});
    }
}