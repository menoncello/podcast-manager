using System.Threading.Tasks;
using PodcastManager.FeedUpdater.Domain.Models;
using PodcastManager.FeedUpdater.Domain.Repositories;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles.EpisodeRepository;

public class EpisodeRepositoryDummy : IEpisodeRepository
{
    public virtual Task<(int, int)> Save(int code, Item[] feedItems) =>
        Task.FromResult((0, 0));

    public virtual Task<int> EpisodeCount(int code) => Task.FromResult(0);
}