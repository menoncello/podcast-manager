using System.Threading.Tasks;
using PodcastManager.Administration.Domain.Repositories;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PlaylistRepositoryStub : IPlaylistRepository
{
    public virtual Task<int[]> ListRelatedPodcasts()
    {
        return Task.FromResult(new[]
        {
            1,
            2,
            3,
            4,
            5
        });
    }
}