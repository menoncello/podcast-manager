using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Messages;
using PodcastManager.Tests.Spies;

namespace PodcastManager.ItunesCrawler.Doubles.Adapters.Itunes;

public class ItunesSpy : ItunesStub
{
    public SpyHelper GetGenresSpy { get; } = new();
    public SpyHelper<Letter> GetTotalPagesSpy { get; } = new();

    public override Task<AppleGenre[]> GetGenres()
    {
        GetGenresSpy.Call();
        return base.GetGenres();
    }

    public override Task<short> GetTotalPages(Letter letter)
    {
        GetTotalPagesSpy.Call(letter);
        return base.GetTotalPages(letter);
    }
}