using PodcastManager.Feed.Adapters;

namespace PodcastManager.Feed.Application.Doubles;

public class FeedStub : IFeedAdapter
{
    public static string Rss => "<rss><channel><title>Sample RSS</title></channel></rss>";

    public virtual string Build(Domain.Models.Feed feed)
    {
        return Rss;
    }
}