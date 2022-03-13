using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastManager.Domain.Models;
using PodcastManager.FeedUpdater.Messages;
using PodcastManager.Tests.Spies;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles.PodcastRepository;

public class PodcastRepositorySpy : PodcastRepositoryStub
{
    public SpyHelper ListPodcastToUpdateSpy { get; } = new();
    public SpyHelper ListPublishedPodcastToUpdateSpy { get; } = new();
    public SpyHelper<(int, Domain.Models.Feed)> SaveFeedDataSpy { get; } = new();
    public SpyHelper<(int, PodcastStatus, string)> UpdateStatusSpy { get; } = new();

    public override Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate()
    {
        ListPodcastToUpdateSpy.Call();
        return base.ListPodcastToUpdate();
    }

    public override Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate()
    {
        ListPublishedPodcastToUpdateSpy.Call();
        return base.ListPublishedPodcastToUpdate();
    }

    public override Task SaveFeedData(int code, Domain.Models.Feed feed)
    {
        SaveFeedDataSpy.Call((code, feed));
        return base.SaveFeedData(code, feed);
    }

    public override Task UpdateStatus(int code, PodcastStatus status, string errorMessage = "")
    {
        UpdateStatusSpy.Call((code, status, errorMessage));
        return base.UpdateStatus(code, status, errorMessage);
    }
}