using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;
using PodcastManager.Tests.Spies;

namespace PodcastManager.ItunesCrawler.Doubles.Adapters.Enqueuer;

public class EnqueuerSpy : IEnqueuerAdapter
{
    public SpyHelper<Letter> EnqueueLetterSpy { get; } = new();
    public SpyHelper<Page> EnqueuePageSpy { get; } = new();

    public Task EnqueueLetter(Letter letter)
    {
        EnqueueLetterSpy.Call(letter);
        return Task.CompletedTask;
    }

    public Task EnqueuePage(Page page)
    {
        EnqueuePageSpy.Call(page);
        return Task.CompletedTask;
    }
}