using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Domain.Models;
using PodcastManager.Tests.Spies;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles;

public class FeedSpy : FeedStub
{
    public SpyHelper<string> GetSpy { get; } = new();
    public override Task<Feed> Get(string feedUrl)
    {
        GetSpy.Call(feedUrl);
        return base.Get(feedUrl);
    }
}