using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Doubles.Adapters.Itunes;

public class ItunesStub : IItunesAdapter
{
    public virtual Task<AppleGenre[]> GetGenres()
    {
        return Task.FromResult(new[]
        {
            new AppleGenre(1, "Genre 1"),
            new AppleGenre(2, "Genre 2"),
            new AppleGenre(3, "Genre 3")
        });
    }

    public virtual Task<short> GetTotalPages(Letter letter) => 
        Task.FromResult((short) 4);
}