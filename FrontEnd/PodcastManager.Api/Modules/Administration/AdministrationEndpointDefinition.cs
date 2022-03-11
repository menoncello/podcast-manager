using PodcastManager.Administration;
using PodcastManager.Administration.CrossCutting.Rabbit;
using PodcastManager.Api.Definitions;
using RabbitMQ.Client;

namespace PodcastManager.Api.Modules.Administration;

public class AdministrationEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
    }

    public void DefineServices(IServiceCollection services)
    {
        services
            .AddSingleton<IAdministrationEnqueuerAdapter>(s =>
            {
                var adapter = new RabbitAdministrationEnqueuerAdapter();
                var factory = s.GetService<IConnectionFactory>()!;
                adapter.SetConnection(factory.CreateConnection());
                return adapter;
            });
    }
}