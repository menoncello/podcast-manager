using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using PodcastManager.ItunesCrawler.Doubles.Adapters.Enqueuer;
using PodcastManager.ItunesCrawler.Doubles.Adapters.Itunes;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class GenreServiceTests
{
    private GenreService service = null!;
    private ItunesSpy itunesSpy = null!;
    private EnqueuerSpy enqueuerSpy = null!;

    [SetUp]
    public void SetUp() => CreateService();

    private void CreateService()
    {
        itunesSpy = new ItunesSpy();
        enqueuerSpy = new EnqueuerSpy();
        
        service = new GenreService();
        service.SetItunes(itunesSpy);
        service.SetEnqueuer(enqueuerSpy);
    }

    [Test]
    public void Constructor_InheritsFromGenreInteractor()
    {
        service.Should().BeAssignableTo<IGenreInteractor>();
    }

    [Test]
    public async Task Execute_ShouldCallGetGenreFromItunesOnce()
    {
        await service.Execute();
        itunesSpy.ListGenresSpy.ShouldBeCalledOnce();
        enqueuerSpy.EnqueueLetterSpy.ShouldBeCalled(81);
        enqueuerSpy.EnqueueLetterSpy.Parameters.First()
            .Should().Be(new Letter(new AppleGenre(1, "Genre 1"), 'A'));
        enqueuerSpy.EnqueueLetterSpy.LastParameter
            .Should().Be(new Letter(new AppleGenre(3, "Genre 3"), '#'));
    }
}

