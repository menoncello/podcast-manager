using PodcastManager.Administration.Domain.Interactors;
using PodcastManager.Administration.Domain.Repositories;

namespace PodcastManager.Administration.Application.Services;

public class PodcastPublisherService : IPodcastPublisherInteractor
{
    private IPlaylistRepository playlistRepository = null!;
    private IPodcastRepository podcastRepository = null!;

    public void SetPlaylistRepository(IPlaylistRepository playlistRepository) =>
        this.playlistRepository = playlistRepository;

    public void SetPodcastRepository(IPodcastRepository podcastRepository) =>
        this.podcastRepository = podcastRepository;

    public async Task PublishAllFromPlaylists()
    {
        var codes = await playlistRepository.ListRelatedPodcasts();
        await podcastRepository.PublishPodcasts(codes);
    }
}