using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Domain.Repositories;
using PodcastManager.ItunesCrawler.Models;
using PodcastManager.Tests.Spies;

namespace PodcastManager.ItunesCrawler.Doubles.Repositories.Podcast;

public class PodcastRepositorySpy : IPodcastRepository
{
    public SpyHelper<ApplePodcast[]> UpsertSpy { get; } = new();
    
    public Task Upsert(ApplePodcast[] podcasts)
    {
        UpsertSpy.Call(podcasts);
        return Task.CompletedTask;
    }
}