namespace PodcastManager.Administration.Domain.Repositories;

public interface IPlaylistRepository
{
    Task<int[]> ListRelatedPodcasts();
}