using PodcastManager.Administration.Core.Models;

namespace PodcastManager.Administration.Domain.Repositories;

public interface IPlaylistRepository
{
    Task<Podcast[]> ListRelatedPodcasts();
}