using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Messages;
using Serilog;

namespace PodcastManager.FeedUpdater.Application.Services;

public class PodcastUpdaterService : IPodcastUpdaterInteractor
{
    private ILogger logger = null!;

    public void SetLogger(ILogger logger) => this.logger = logger;
    
    public Task Execute(UpdatePodcast podcast)
    {
        logger.Information("Processing podcast: {Podcast}", podcast.Title);
        return Task.CompletedTask;
    }
}