using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Domain.Models;
using PodcastManager.Tests.Spies;

namespace PodcastManager.FeedUpdater.CrossCutting.Http.Tests.Doubles.FeedCleanerInteractor;

public class FeedConverterSpy : IFeedConverterInteractor
{
    public SpyHelper<string> ProcessSpy { get; } = new();

    public Feed Execute(string data)
    {
        ProcessSpy.Call(data);
        return Feed.Empty();
    }
}