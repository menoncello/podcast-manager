using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles.PodcastRepository;

public class PodcastRepositoryStub : PodcastRepositoryDummy
{
    public readonly UpdatePodcast[] Podcasts = {
        new(1, "Podcast 1", "https://feedpodcast1.com/rss", true),
        new(2, "Podcast 2", "https://feedpodcast2.com/rss", true),
        new(3, "Podcast 3", "https://feedpodcast3.com/rss", false),
        new(4, "Podcast 4", "https://feedpodcast4.com/rss", false),
        new(5, "Podcast 5", "https://feedpodcast5.com/rss", false)
    };
    
    public override Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate()
    {
        return Task.FromResult(Podcasts as IReadOnlyCollection<UpdatePodcast>);
    }

    public override Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate()
    {
        return Task.FromResult(Podcasts.Take(3).ToList() as IReadOnlyCollection<UpdatePodcast>);
    }
}