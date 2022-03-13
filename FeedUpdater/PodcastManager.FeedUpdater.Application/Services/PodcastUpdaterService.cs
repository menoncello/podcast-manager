using PodcastManager.FeedUpdater.CrossCutting.Http;
using PodcastManager.FeedUpdater.Domain.Adapters;
using PodcastManager.FeedUpdater.Domain.Exceptions;
using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Domain.Models;
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
        var feed = await TryGetFeedData();
        logger.Information("Processing podcast: {Podcast}", title);

        async Task<Feed> TryGetFeedData()
        {
            try
            {
                return await feedAdapter.Get(feedUrl);
            }
            catch (ServerErrorException e)
            {
                logger.Error("Server error ({ErrorCode} {ErrorReason}) for {Podcast}",
                    e.Code,
                    e.Reason,
                    podcast);
                return Feed.Empty();
            }
            catch (TryToParseErrorException)
            {
                logger.Error("Parsing XML error for {Podcast}",
                    podcast);
                return Feed.Empty();
            }
            catch (Exception e)
            {
                logger.Error(e, "generic error with {Podcast}", podcast);
                throw;
            }
        }
    }
}