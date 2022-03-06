using MongoDB.Driver;
using PodcastManager.ItunesCrawler.Domain.Repositories;
using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.CrossCutting.Mongo;

public class MongoPodcastRepository : IPodcastRepository
{
    private IMongoDatabase database = null!;

    public void SetDatabase(IMongoDatabase database) =>
        this.database = database;
    
    public async Task Upsert(Podcast[] podcasts)
    {
        var collection = database.GetCollection<Podcast>("podcasts");

        var codes = podcasts.Select(x => x.Code).ToList();
        var @new = podcasts.Except(await GetExistingPodcasts()).ToList();
        var requests = new List<WriteModel<Podcast>>(podcasts.Length);

        requests.AddRange(@new.Select(x => new InsertOneModel<Podcast>(x)));
        requests.AddRange((await GetExistingPodcasts()).Select(CreateUpdateModel));

        var response = await collection.BulkWriteAsync(requests);

        UpdateOneModel<Podcast> CreateUpdateModel(Podcast p)
        {
            var filter = new FilterDefinitionBuilder<Podcast>().Eq(x => x.Code, p.Code);
            var update = Builders<Podcast>.Update.Set(x => x.Imported.Genres, p.Imported.Genres)
                .Set(x => x.Imported.ArtistId, p.Imported.ArtistId)
                .Set(x => x.Imported.ArtworkUrl600, p.Imported.ArtworkUrl600)
                .Set(x => x.Imported.CollectionExplicitness, p.Imported.CollectionExplicitness)
                .Set(x => x.Imported.CollectionId, p.Imported.CollectionId)
                .Set(x => x.Imported.CollectionName, p.Imported.CollectionName)
                .Set(x => x.Imported.FeedUrl, p.Imported.FeedUrl)
                .Set(x => x.Imported.GenreIds, p.Imported.GenreIds)
                .Set(x => x.Imported.ContentAdvisoryRating, p.Imported.ContentAdvisoryRating)
                .Set(x => x.Imported.PrimaryGenreName, p.Imported.PrimaryGenreName);
            return new UpdateOneModel<Podcast>(filter, update);
        }

        async Task<List<Podcast>> GetExistingPodcasts()
        {
            return await collection
                .Find(x => codes.Contains(x.Code))
                .ToListAsync();
        }
    }
}