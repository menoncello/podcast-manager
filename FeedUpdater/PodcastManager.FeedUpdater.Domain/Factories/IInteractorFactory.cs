using PodcastManager.FeedUpdater.Domain.Interactors;

namespace PodcastManager.FeedUpdater.Domain.Factories;

public interface IInteractorFactory
{
    Task<IMultiplePodcastUpdaterInteractor> CreateMultiple();
    Task<IPodcastUpdaterInteractor> CreatePodcastUpdater();
}