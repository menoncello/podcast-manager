using System.Threading.Tasks;
using PodcastManager.Tests.Spies;

namespace PodcastManager.Feed.Application.Doubles;

public class PlaylistRepositorySpy : PlaylistRepositoryStub
{
    public SpyHelper<(string, string)> GetFeedSpy { get; } = new();

    public override Task<Domain.Models.Feed> GetFeed(string username, string slug)
    {
        GetFeedSpy.Call((username, slug));
        return base.GetFeed(username, slug);
    }
}