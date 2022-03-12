using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Domain.Adapters;
using PodcastManager.FeedUpdater.Domain.Models;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles;

public class FeedStub : IFeedAdapter
{
    public virtual Task<Feed> Get(string feedUrl)
    {
        return Task.FromResult(Feed.Empty());
    }
}