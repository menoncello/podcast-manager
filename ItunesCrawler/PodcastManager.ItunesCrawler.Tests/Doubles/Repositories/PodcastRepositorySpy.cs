using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Domain.Repositories;
using PodcastManager.ItunesCrawler.Models;
using PodcastManager.Tests.Spies;

namespace PodcastManager.ItunesCrawler.Doubles.Repositories;

public class PodcastRepositorySpy : IPodcastRepository
{
    public SpyHelper<Podcast[]> UpsertSpy { get; } = new();
    
    public Task Upsert(Podcast[] podcasts)
    {
        UpsertSpy.Call(podcasts);
        return Task.CompletedTask;
    }
}