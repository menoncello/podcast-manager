using PodcastManager.Administration.Domain.Interactors;
using PodcastManager.Administration.Domain.Repositories;
using Serilog;
using Serilog.Core;

namespace PodcastManager.Administration.Application.Services;

public class PodcastPublisherService : IPodcastPublisherInteractor
{
    private IPlaylistRepository playlistRepository = null!;
    private IPodcastRepository podcastRepository = null!;
    private ILogger logger = null!;

    public void SetPlaylistRepository(IPlaylistRepository playlistRepository) =>
        this.playlistRepository = playlistRepository;
    public void SetPodcastRepository(IPodcastRepository podcastRepository) =>
        this.podcastRepository = podcastRepository;
    public void SetLogger(ILogger logger) => this.logger = logger;

    public async Task PublishAllFromPlaylists()
    {
        var codes = await playlistRepository.ListRelatedPodcasts();
        var total = await podcastRepository.PublishPodcasts(codes);
        logger.Information("{Total} podcasts was published", total);
    }
}