using Microsoft.AspNetCore.Mvc;
using PodcastManager.Administration;
using PodcastManager.Api.Definitions;

namespace PodcastManager.Api.Modules.Administration.Podcasts.FromPlaylists;

public class FromPlaylistsEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/admin/podcasts/publish/from-playlists", PublishFromPlaylists);
    }
    
    internal static void PublishFromPlaylists([FromServices] IAdministrationEnqueuerAdapter enqueuer) =>
        enqueuer.EnqueuePublishPodcastFromPlaylists();

    public void DefineServices(IServiceCollection services)
    {
    }
}