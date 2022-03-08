using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using PodcastManager.ItunesCrawler.Doubles.Adapters.Enqueuer;
using PodcastManager.ItunesCrawler.Doubles.Adapters.Itunes;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class LetterServiceTests
{
    private LetterService service = null!;
    private EnqueuerSpy enqueuerSpy = null!;
    private ItunesSpy itunesSpy = null!;

    private void CreateService()
    {
        enqueuerSpy = new EnqueuerSpy();
        itunesSpy = new ItunesSpy();
        
        service = new LetterService();
        service.SetEnqueuer(enqueuerSpy);
        service.SetItunes(itunesSpy);
    }

    [SetUp]
    public void SetUp() => CreateService();

    [Test]
    public void Constructor_ShouldInheritsFromLetterInteractor() =>
        service.Should().BeAssignableTo<ILetterInteractor>();

    [Test]
    public async Task Execute_ShouldCallGetPagesOnceAndEnqueuePageFourTimes()
    {
        var letter = new Letter(new AppleGenre(1, "Genre 1"), 'A');
        
        await service.Execute(letter);
        
        itunesSpy.GetTotalPagesSpy.ShouldBeCalledOnce();
        itunesSpy.GetTotalPagesSpy.LastParameter.Should().Be(letter);
        enqueuerSpy.EnqueuePageSpy.ShouldBeCalled(4);
        enqueuerSpy.EnqueuePageSpy.Parameters.Should().BeEquivalentTo(new[]
        {
            new Page(letter, 1),
            new Page(letter, 2),
            new Page(letter, 3),
            new Page(letter, 4),
        });
    }
}