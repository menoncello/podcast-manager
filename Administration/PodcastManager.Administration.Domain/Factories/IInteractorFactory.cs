using PodcastManager.Administration.Domain.Interactors;

namespace PodcastManager.Administration.Domain.Factories;

public interface IInteractorFactory
{
    IPodcastPublisherInteractor CreatePodcastPublisher();
}