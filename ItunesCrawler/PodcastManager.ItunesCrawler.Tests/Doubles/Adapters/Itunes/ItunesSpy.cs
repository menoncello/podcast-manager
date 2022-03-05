using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Application.Services;
using PodcastManager.ItunesCrawler.Messages;
using PodcastManager.Tests.Spies;

namespace PodcastManager.ItunesCrawler.Doubles.Adapters.Itunes;

public class ItunesSpy : ItunesStub
{
    public SpyHelper GetGenresSpy { get; } = new();
    public override Task<AppleGenre[]> GetGenres()
    {
        GetGenresSpy.Call();
        return base.GetGenres();
    }
}