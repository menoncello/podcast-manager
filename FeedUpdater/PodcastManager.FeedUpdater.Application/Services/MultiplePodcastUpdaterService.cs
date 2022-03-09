using PodcastManager.FeedUpdater.Adapters;
using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Domain.Repositories;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Application.Services;

public class MultiplePodcastUpdaterService : IMultiplePodcastUpdaterInteractor
{
    private IPodcastRepository repository = null!;
    private IUpdaterEnqueuerAdapter enqueuer = null!;

    public void SetRepository(IPodcastRepository repository) => this.repository = repository;
    public void SetEnqueuer(IUpdaterEnqueuerAdapter enqueuer) => this.enqueuer = enqueuer;

    public async Task Execute() =>
        EnqueuePodcasts(await repository.ListPodcastToUpdate());

    public async Task ExecutePublished() =>
        EnqueuePodcasts(await repository.ListPublishedPodcastToUpdate());

    private void EnqueuePodcasts(UpdatePodcast[] podcasts)
    {
        foreach (var podcast in podcasts) 
            enqueuer.EnqueueUpdatePodcast(podcast);
    }
}