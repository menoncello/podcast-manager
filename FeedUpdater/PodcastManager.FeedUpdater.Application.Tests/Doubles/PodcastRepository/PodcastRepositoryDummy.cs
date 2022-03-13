using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastManager.Domain.Models;
using PodcastManager.FeedUpdater.Domain.Repositories;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.Application.Tests.Doubles.PodcastRepository;

public class PodcastRepositoryDummy : IPodcastRepository
{
    private readonly IReadOnlyCollection<UpdatePodcast> nothing = new List<UpdatePodcast>().AsReadOnly();
    public virtual Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate() =>
        Task.FromResult(nothing);

    public virtual Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate() =>
        Task.FromResult(nothing);

    public virtual Task SaveFeedData(int code, Domain.Models.Feed feed) => Task.CompletedTask;
    public virtual Task UpdateStatus(int code, PodcastStatus status, string errorMessage = "") => Task.CompletedTask;
}