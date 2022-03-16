using Microsoft.AspNetCore.Mvc;
using PodcastManager.Api.Definitions;
using PodcastManager.Feed.Domain.Interactors;

namespace PodcastManager.Api.Modules.Feed.Playlists;

public class PlaylistsEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/feed/{username}/playlist/{slug}", FeedFromPlaylist);
    }

    private static async Task<IResult> FeedFromPlaylist([FromServices] IPlaylistInteractor interactor,
        string username, string slug) =>
        Results.Text(await interactor.FromPlaylist(username, slug), "text/xml");

    public void DefineServices(IServiceCollection services)
    {
    }
}