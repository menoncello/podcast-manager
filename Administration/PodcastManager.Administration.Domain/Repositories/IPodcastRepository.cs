namespace PodcastManager.Administration.Domain.Repositories;

public interface IPodcastRepository
{
    Task<long> PublishPodcasts(int[] codes);
}