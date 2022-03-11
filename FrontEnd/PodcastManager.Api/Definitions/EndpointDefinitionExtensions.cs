namespace PodcastManager.Api.Definitions;

public static class EndpointDefinitionExtensions
{
    public static IServiceCollection AddEndpointDefinitions(
        this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpoints = new List<IEndpointDefinition>();

        foreach (var marker in scanMarkers)
            endpoints.AddRange(GetEndpointsFromAssembly(marker));

        foreach (var endpoint in endpoints) 
            endpoint.DefineServices(services);

        services.AddSingleton(endpoints as IReadOnlyCollection<IEndpointDefinition>);

        IEnumerable<IEndpointDefinition> GetEndpointsFromAssembly(Type marker) =>
            marker.Assembly.ExportedTypes
                .Where(IsEndpointDefinitionAndConcreteClass)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

        bool IsEndpointDefinitionAndConcreteClass(Type type) =>
            typeof(IEndpointDefinition).IsAssignableFrom(type) &&
            !type.IsInterface &&
            !type.IsAbstract;

        return services;
    }
    
    public static IApplicationBuilder UseEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services
            .GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var definition in definitions) 
            definition.DefineEndpoints(app);

        return app;
    }
}