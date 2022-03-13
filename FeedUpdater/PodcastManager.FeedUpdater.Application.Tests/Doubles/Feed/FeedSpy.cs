using System.Threading.Tasks;
using PodcastManager.Tests.Spies;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles.Feed;

public class FeedSpy : FeedStub
{
    public SpyHelper<string> GetSpy { get; } = new();
    public override Task<Domain.Models.Feed> Get(string feedUrl)
    {
        GetSpy.Call(feedUrl);
        return base.Get(feedUrl);
    }
}