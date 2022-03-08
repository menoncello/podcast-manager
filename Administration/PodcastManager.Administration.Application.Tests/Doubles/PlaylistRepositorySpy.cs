using System.Threading.Tasks;
using PodcastManager.Tests.Spies;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PlaylistRepositorySpy : PlaylistRepositoryStub
{
    public SpyHelper ListRelatedPodcastsSpy { get; } = new();
    
    public override Task<int[]> ListRelatedPodcasts()
    {
        ListRelatedPodcastsSpy.Call();
        return base.ListRelatedPodcasts();
    }
}