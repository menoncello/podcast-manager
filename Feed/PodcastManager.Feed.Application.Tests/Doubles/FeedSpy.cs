using PodcastManager.Tests.Spies;

namespace PodcastManager.Feed.Application.Doubles;

public class FeedSpy : FeedStub
{
    public SpyHelper<Domain.Models.Feed> BuildSpy { get; } = new();

    public override string Build(Domain.Models.Feed feed)
    {
        BuildSpy.Call(feed);
        return base.Build(feed);
    }
}