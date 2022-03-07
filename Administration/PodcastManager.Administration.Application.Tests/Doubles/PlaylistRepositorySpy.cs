using System.Threading.Tasks;
using PodcastManager.Administration.Core.Models;
using PodcastManager.Tests.Spies;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PlaylistRepositorySpy : PlaylistRepositoryStub
{
    public SpyHelper ListRelatedPodcastsSpy { get; } = new();
    
    public override Task<Podcast[]> ListRelatedPodcasts()
    {
        ListRelatedPodcastsSpy.Call();
        return base.ListRelatedPodcasts();
    }
}