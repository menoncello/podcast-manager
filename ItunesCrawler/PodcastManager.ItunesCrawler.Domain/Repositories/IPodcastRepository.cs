using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.Domain.Repositories;

public interface IPodcastRepository
{
    Task Upsert(Podcast[] podcasts);
}