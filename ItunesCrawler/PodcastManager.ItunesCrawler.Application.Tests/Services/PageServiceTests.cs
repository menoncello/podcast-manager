using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using PodcastManager.ItunesCrawler.Doubles.Adapters.Itunes;
using PodcastManager.ItunesCrawler.Doubles.Repositories.Podcast;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class PageServiceTests
{
    private PageService service = null!;
    private ItunesSpy itunesSpy = null!;
    private PodcastRepositorySpy repositorySpy = null!;

    private void CreateService()
    {
        itunesSpy = new ItunesSpy();
        repositorySpy = new PodcastRepositorySpy();
        
        service = new PageService();
        service.SetItunes(itunesSpy);
        service.SetRepository(repositorySpy);
    }

    [SetUp]
    public void SetUp() => CreateService();

    [Test]
    public void Constructor_ShouldInheritsFromPageInteractor() =>
        service.Should().BeAssignableTo<IPageInteractor>();

    [Test]
    public async Task Execute_ShouldCallPodcastsFromPageOncePodcastsOnceAndUpsertOnce()
    {
        var page = new Page(new Letter(new AppleGenre(1, "Genre 1"), 'A'), 2);
        await service.Execute(page);
        
        itunesSpy.PodcastsFromPageSpy.ShouldBeCalledOnce();
        itunesSpy.PodcastsFromPageSpy.LastParameter.Should().BeEquivalentTo(page);
        
        itunesSpy.GetPodcastsSpy.ShouldBeCalledOnce();
        itunesSpy.GetPodcastsSpy.LastParameter.Should().BeEquivalentTo(itunesSpy.PodcastCodes);
        
        repositorySpy.UpsertSpy.ShouldBeCalledOnce();
        repositorySpy.UpsertSpy.LastParameter.Should().BeEquivalentTo(itunesSpy.Podcasts);
    }
}