using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Messages;
using PodcastManager.Tests.Spies;

namespace PodcastManager.FeedUpdater.Application.Tests.Services;

public class PodcastRepositorySpy : PodcastRepositoryStub
{
    public SpyHelper ListPodcastToUpdateSpy { get; } = new();
    public SpyHelper ListPublishedPodcastToUpdateSpy { get; } = new();

    public override Task<UpdatePodcast[]> ListPodcastToUpdate()
    {
        ListPodcastToUpdateSpy.Call();
        return base.ListPodcastToUpdate();
    }

    public override async Task<UpdatePodcast[]> ListPublishedPodcastToUpdate()
    {
        ListPublishedPodcastToUpdateSpy.Call();
        return await base.ListPublishedPodcastToUpdate();
    }
}