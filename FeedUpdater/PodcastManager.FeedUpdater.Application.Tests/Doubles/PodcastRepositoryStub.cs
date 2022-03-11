using System.Linq;
using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Domain.Repositories;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles;

public class PodcastRepositoryStub : IPodcastRepository
{
    public readonly UpdatePodcast[] Podcasts = {
        new(1, "Podcast 1", "https://feedpodcast1.com/rss"),
        new(2, "Podcast 2", "https://feedpodcast2.com/rss"),
        new(3, "Podcast 3", "https://feedpodcast3.com/rss"),
        new(4, "Podcast 4", "https://feedpodcast4.com/rss"),
        new(5, "Podcast 5", "https://feedpodcast5.com/rss")
    };
    
    public virtual Task<UpdatePodcast[]> ListPodcastToUpdate()
    {
        return Task.FromResult(Podcasts);
    }

    public virtual Task<UpdatePodcast[]> ListPublishedPodcastToUpdate()
    {
        return Task.FromResult(Podcasts.Take(3).ToArray());
    }
}