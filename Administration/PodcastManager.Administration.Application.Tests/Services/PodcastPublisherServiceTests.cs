using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PodcastManager.Administration.Application.Services;
using PodcastManager.Administration.Application.Tests.Doubles;
using PodcastManager.Administration.Domain.Interactors;

namespace PodcastManager.Administration.Application.Tests.Services;

public class PodcastPublisherServiceTests
{
    private IPodcastPublisherInteractor service = null!;
    private PlaylistRepositorySpy playlistRepositorySpy = null!; 
    private PodcastRepositorySpy podcastRepositorySpy = null!; 

    private void CreateService()
    {
        playlistRepositorySpy = new PlaylistRepositorySpy();
        podcastRepositorySpy = new PodcastRepositorySpy();
        
        var currentService = new PodcastPublisherService();
        currentService.SetPlaylistRepository(playlistRepositorySpy);
        currentService.SetPodcastRepository(podcastRepositorySpy);
        service = currentService;
    }

    [SetUp]
    public void SetUp() => CreateService();

    [Test]
    public void Constructor_ShouldInheritsPodcastInteractor() =>
        service.Should().BeAssignableTo<IPodcastPublisherInteractor>();

    [Test]
    public async Task PublishAllFromPlaylists_RepositoryGetAllPodcastRelatedCalledOnce()
    {
        await service.PublishAllFromPlaylists();
        
        playlistRepositorySpy.ListRelatedPodcastsSpy.ShouldBeCalledOnce();
        podcastRepositorySpy.PublishPodcastsSpy.ShouldBeCalledOnce();
        podcastRepositorySpy.PublishPodcastsSpy.LastParameter.Should()
            .BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
    }
}