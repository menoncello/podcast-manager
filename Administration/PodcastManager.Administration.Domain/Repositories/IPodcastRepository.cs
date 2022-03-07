namespace PodcastManager.Administration.Domain.Repositories;

public interface IPodcastRepository
{
    Task PublishPodcasts(int[] codes);
}