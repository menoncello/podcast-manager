using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using PodcastManager.ItunesCrawler.Domain.Repositories;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class PageService : IPageInteractor
{
    private IItunesAdapter itunes = null!;
    private IPodcastRepository repository = null!;

    public async Task Execute(Page page)
    {
        var codes = await itunes.PodcastsFromPage(page);
        var podcasts = await itunes.GetPodcasts(codes);
        await repository.Upsert(podcasts);
    }

    public void SetItunes(IItunesAdapter itunes) => this.itunes = itunes;
    public void SetRepository(IPodcastRepository repository) => this.repository = repository;
}