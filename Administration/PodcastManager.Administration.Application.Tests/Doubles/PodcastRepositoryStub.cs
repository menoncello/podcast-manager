using System.Threading.Tasks;
using PodcastManager.Administration.Domain.Repositories;

namespace PodcastManager.Administration.Application.Tests.Doubles;

public class PodcastRepositoryStub : IPodcastRepository
{
    public virtual Task PublishPodcasts(int[] codes)
    {
        return Task.CompletedTask;
    }
}