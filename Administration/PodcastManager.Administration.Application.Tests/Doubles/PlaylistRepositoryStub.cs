using System.Threading.Tasks;
using PodcastManager.Administration.Core.Models;
using PodcastManager.Administration.Domain.Repositories;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PlaylistRepositoryStub : IPlaylistRepository
{
    public virtual Task<Podcast[]> ListRelatedPodcasts()
    {
        return Task.FromResult(new[]
        {
            new Podcast(1),
            new Podcast(2),
            new Podcast(3),
            new Podcast(4),
            new Podcast(5),
        });
    }
}