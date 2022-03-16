using System.Threading.Tasks;
using PodcastManager.Feed.Domain.Repositories;

namespace PodcastManager.Feed.Application.Doubles;

public class PlaylistRepositoryStub : IPlaylistRepository
{
    public Domain.Models.Feed Feed { get; } = new("Test Feed 1");

    public virtual Task<Domain.Models.Feed> GetFeed(string username, string slug)
    {
        return Task.FromResult(Feed);
    }
}