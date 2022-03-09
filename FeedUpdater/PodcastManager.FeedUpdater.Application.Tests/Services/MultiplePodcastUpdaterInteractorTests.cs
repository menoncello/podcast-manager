using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PodcastManager.FeedUpdater.Application.Services;
using PodcastManager.FeedUpdater.Domain.Interactors;

namespace PodcastManager.FeedUpdater.Application.Tests.Services;

public class MultiplePodcastUpdaterInteractorTests
{
    private IMultiplePodcastUpdaterInteractor service = null!;
    private PodcastRepositorySpy podcastRepositorySpy = null!;
    private UpdaterEnqueuerSpy updaterEnqueuerSpy = null!;

    private void CreateService()
    {
        podcastRepositorySpy = new PodcastRepositorySpy();
        updaterEnqueuerSpy = new UpdaterEnqueuerSpy();
        
        var newService = new MultiplePodcastUpdaterService();
        newService.SetRepository(podcastRepositorySpy);
        newService.SetEnqueuer(updaterEnqueuerSpy);
        service = newService;
    }

    [SetUp]
    public void SetUp()
    {
        CreateService();
    }

    [Test]
    public void Constructor_ShouldInheritsMultiplePodcastUpdaterInteractor()
    {
        service.Should().BeAssignableTo<IMultiplePodcastUpdaterInteractor>();
    }

    [Test]
    public async Task Execute_ShouldCallListPodcastToUpdateOnceAndEnqueueUpdatePodcast5Times()
    {
        await service.Execute();
        podcastRepositorySpy.ListPodcastToUpdateSpy.ShouldBeCalledOnce();
        updaterEnqueuerSpy.EnqueueUpdatePodcastSpy.ShouldBeCalled(5);
        updaterEnqueuerSpy.EnqueueUpdatePodcastSpy.Parameters
            .Should().BeEquivalentTo(podcastRepositorySpy.Podcasts);
    }

    [Test]
    public async Task ExecutePublished_ShouldCallListPublishedPodcastToUpdateOnceAndEnqueueUpdatePodcast3Times()
    {
        await service.ExecutePublished();
        podcastRepositorySpy.ListPublishedPodcastToUpdateSpy.ShouldBeCalledOnce();
        updaterEnqueuerSpy.EnqueueUpdatePodcastSpy.ShouldBeCalled(3);
        updaterEnqueuerSpy.EnqueueUpdatePodcastSpy.Parameters
            .Should().BeEquivalentTo(podcastRepositorySpy.Podcasts.Take(3));
    }
}