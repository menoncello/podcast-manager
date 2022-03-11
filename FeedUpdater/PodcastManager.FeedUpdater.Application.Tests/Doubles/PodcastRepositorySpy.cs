using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Messages;
using PodcastManager.Tests.Spies;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles;

public class PodcastRepositorySpy : PodcastRepositoryStub
{
    public SpyHelper ListPodcastToUpdateSpy { get; } = new();
    public SpyHelper ListPublishedPodcastToUpdateSpy { get; } = new();

    public override Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate()
    {
        ListPodcastToUpdateSpy.Call();
        return base.ListPodcastToUpdate();
    }

    public override async Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate()
    {
        ListPublishedPodcastToUpdateSpy.Call();
        return await base.ListPublishedPodcastToUpdate();
    }
}