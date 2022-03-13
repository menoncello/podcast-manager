using MongoDB.Driver;
using PodcastManager.Adapters;
using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.Domain.Models;
using PodcastManager.FeedUpdater.Domain.Models;
using PodcastManager.FeedUpdater.Domain.Repositories;
using PodcastManager.FeedUpdater.Messages;
using Serilog;

namespace PodcastManager.FeedUpdater.CrossCutting.Mongo;

public class MongoPodcastRepository : MongoRepository, IPodcastRepository
{
    private IDateTimeAdapter dateTime = null!;
    private ILogger logger = null!;
    
    private readonly ExpressionFilterDefinition<FullPodcast> isPublished = new(x => x.IsPublished);

    public void SetDateTime(IDateTimeAdapter dateTime) => this.dateTime = dateTime;
    public void SetLogger(ILogger logger) => this.logger = logger;

    public Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastToUpdate() =>
        ListPodcastsToUpdate(GetNeedsUpdate(dateTime.Now()));

    public Task<IReadOnlyCollection<UpdatePodcast>> ListPublishedPodcastToUpdate() =>
        ListPodcastsToUpdate(isPublished, GetNeedsUpdate(dateTime.Now()));

    public Task SaveFeedData(int code, Feed feed)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStatus(int code, PodcastStatus status, string errorMessage = "")
    {
        throw new NotImplementedException();
    }

    private async Task<IReadOnlyCollection<UpdatePodcast>> ListPodcastsToUpdate(
        params FilterDefinition<FullPodcast>[] filters)
    {
        var collection = GetCollection<FullPodcast>("podcasts");
        var cursor = await collection
            .Find(Builders<FullPodcast>.Filter.And(filters), new FindOptions {BatchSize = 100000})
            .Project(x => new UpdatePodcast(x.Code, x.Title, x.Feed, x.IsPublished, x.Status!.ErrorCount))
            .ToCursorAsync();

        var result = new List<UpdatePodcast>();
        var page = 1;

        while (await cursor.MoveNextAsync())
        {
            result.AddRange(cursor.Current);
            logger.Debug("Required page {Page}", page);
            page++;
        }

        return result;
    }

    private static ExpressionFilterDefinition<FullPodcast> GetNeedsUpdate(DateTime now) =>
        new(x => x.Status == null || x.Status.NextUpdate <= now );
}