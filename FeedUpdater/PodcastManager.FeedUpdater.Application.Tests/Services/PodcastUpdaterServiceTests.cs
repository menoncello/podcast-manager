using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PodcastManager.Doubles;
using PodcastManager.FeedUpdater.Application.Services;
using PodcastManager.FeedUpdater.Application.Tests.Doubles;
using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Application.Tests.Services;

public class PodcastUpdaterServiceTests
{
    private IPodcastUpdaterInteractor service = null!;
    private FeedSpy feedSpy = null!;

    private void CreateService()
    {
        feedSpy = new FeedSpy();
        var newService = new PodcastUpdaterService();
        newService.SetFeed(feedSpy);
        newService.SetLogger(new LoggerDummy());
        service = newService;
    }
    
    [SetUp]
    public void SetUp()
    {
        CreateService();
    }

    [Test]
    public void Constructor_InheritsFromPodcastUpdaterInteractor() =>
        service.Should().BeAssignableTo<IPodcastUpdaterInteractor>();

    [Test]
    public async Task Execute_CallGetAtFeedAdapterOnce()
    {
        var updatePodcast = new UpdatePodcast(1, "Podcast 1", "https://feed.podcast1.com");
        await service.Execute(updatePodcast);
        
        feedSpy.GetSpy.ShouldBeCalledOnce();
        feedSpy.GetSpy.LastParameter.Should().Be(updatePodcast.Feed);
    }
}