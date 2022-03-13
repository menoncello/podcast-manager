using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Domain.Models;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles.EpisodeRepository;

public class EpisodeRepositoryStub : EpisodeRepositoryDummy
{
    public override Task<(int, int)> Save(int code, Item[] feedItems) => Task.FromResult((1, 1));
    public override Task<int> EpisodeCount(int code) => Task.FromResult(15);
}