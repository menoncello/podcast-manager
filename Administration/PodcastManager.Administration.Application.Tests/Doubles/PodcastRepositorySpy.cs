using System.Threading.Tasks;
using PodcastManager.Tests.Spies;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PodcastRepositorySpy : PodcastRepositoryStub
{
    public SpyHelper<int[]> PublishPodcastsSpy { get; } = new();
    public override Task PublishPodcasts(int[] codes)
    {
        PublishPodcastsSpy.Call(codes);
        return base.PublishPodcasts(codes);
    }
}