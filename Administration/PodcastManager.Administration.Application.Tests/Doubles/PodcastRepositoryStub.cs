using System.Threading.Tasks;
using PodcastManager.Administration.Domain.Repositories;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PodcastRepositoryStub : IPodcastRepository
{
    public virtual Task<long> PublishPodcasts(int[] codes)
    {
        return Task.FromResult(10L);
    }
}