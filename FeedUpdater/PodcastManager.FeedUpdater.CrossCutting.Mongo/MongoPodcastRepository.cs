using MongoDB.Driver;
using PodcastManager.Adapters;
using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.Domain.Models;
using PodcastManager.FeedUpdater.Domain.Repositories;
using PodcastManager.FeedUpdater.Messages;

namespace PodcastManager.FeedUpdater.CrossCutting.Mongo;

public class MongoPodcastRepository : MongoRepository, IPodcastRepository
{
    private IDateTimeAdapter dateTime = null!;
    private readonly ExpressionFilterDefinition<FullPodcast> isPublished = new(x => x.IsPublished);

    public void SetDateTime(IDateTimeAdapter dateTime) => this.dateTime = dateTime;

    public Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate() =>
        ListPodcastsToUpdate(GetNeedsUpdate(dateTime.Now()));

    public Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate() =>
        ListPodcastsToUpdate(isPublished, GetNeedsUpdate(dateTime.Now()));

    private async Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastsToUpdate(
        params FilterDefinition<FullPodcast>[] filters)
    {
        var collection = GetCollection<FullPodcast>("podcasts");
        var cursor = await collection
            .Find(Builders<FullPodcast>.Filter.And(filters), new FindOptions {BatchSize = 100})
            .Project(x => new UpdatePodcast(x.Code, x.Title, x.Feed))
            .ToCursorAsync();

        var result = new List<UpdatePodcast>(cursor.Current);

        while (await cursor.MoveNextAsync())
            result.AddRange(cursor.Current);

        return result;
    }

    private static ExpressionFilterDefinition<FullPodcast> GetNeedsUpdate(DateTime now) =>
        new(x => x.Status == null || x.Status.NextUpdate <= now );
}