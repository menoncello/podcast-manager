using PodcastManager.FeedUpdater.Domain.Adapters;
using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Messages;
using Serilog;

namespace PodcastManager.FeedUpdater.Application.Services;

public class PodcastUpdaterService : IPodcastUpdaterInteractor
{
    private ILogger logger = null!;
    private IFeedAdapter feedAdapter = null!;

    public void SetLogger(ILogger logger) => this.logger = logger;
    public void SetFeed(IFeedAdapter feedAdapter) => this.feedAdapter = feedAdapter;

    public async Task Execute(UpdatePodcast podcast)
    {
        var (code, title, feedUrl) = podcast;
        var feed = await feedAdapter.Get(feedUrl);
        logger.Information("Processing podcast: {Podcast}", title);
    }
}