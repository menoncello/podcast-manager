using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using PodcastManager.ItunesCrawler.Domain.Repositories;
using PodcastManager.ItunesCrawler.Messages;
using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class PageService : IPageInteractor
{
    private IItunesAdapter itunes = null!;
    private IPodcastRepository repository = null!;

    public async Task Execute(Page page)
    {
        var codes = await itunes.PodcastsFromPage(page);
        var applePodcasts = await itunes.GetPodcasts(codes);
        var podcasts = applePodcasts
            .Select(Podcast.FromApple)
            .ToArray();
        await repository.Upsert(podcasts);
    }

    public void SetItunes(IItunesAdapter itunes) => this.itunes = itunes;
    public void SetRepository(IPodcastRepository repository) => this.repository = repository;
}