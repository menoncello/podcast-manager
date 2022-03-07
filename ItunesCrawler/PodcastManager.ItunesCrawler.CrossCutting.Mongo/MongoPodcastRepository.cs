using MongoDB.Driver;
using PodcastManager.ItunesCrawler.CrossCutting.Mongo.Data;
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
        var collection = database.GetCollection<PodcastData>("podcasts");
        var data = podcasts.Select(PodcastData.FromPodcast).ToList();

        var codes = data.Select(x => x.Code).ToArray();
        var existingPodcasts = await GetExistingPodcasts(collection, codes);
        var existingCodes = existingPodcasts.Select(x => x.Code).ToList();
        var @new = data.Where(x => !existingCodes.Contains(x.Code)).ToList();
        var requests = new List<WriteModel<PodcastData>>(podcasts.Length);

        requests.AddRange(@new.Select(x => new InsertOneModel<PodcastData>(x)));
        requests.AddRange(existingPodcasts.Select(CreateUpdateModel));

        if (!requests.Any()) return;

        var response = await collection.BulkWriteAsync(requests);
        
        Console.WriteLine($"{DateTime.Now} - Total podcasts: {podcasts.Length} - new: {@new.Count} - updated: {response.ModifiedCount}");

        UpdateOneModel<PodcastData> CreateUpdateModel(PodcastData podcast)
        {
            var filter = new FilterDefinitionBuilder<PodcastData>().Eq(x => x.Code, podcast.Code);
            var update = Builders<PodcastData>.Update.Set(x => x.Imported.Genres, podcast.Imported.Genres)
                .Set(x => x.Imported.ArtistId, podcast.Imported.ArtistId)
                .Set(x => x.Imported.ArtworkUrl600, podcast.Imported.ArtworkUrl600)
                .Set(x => x.Imported.CollectionExplicitness, podcast.Imported.CollectionExplicitness)
                .Set(x => x.Imported.CollectionId, podcast.Imported.CollectionId)
                .Set(x => x.Imported.CollectionName, podcast.Imported.CollectionName)
                .Set(x => x.Imported.FeedUrl, podcast.Imported.FeedUrl)
                .Set(x => x.Imported.GenreIds, podcast.Imported.GenreIds)
                .Set(x => x.Imported.ContentAdvisoryRating, podcast.Imported.ContentAdvisoryRating)
                .Set(x => x.Imported.PrimaryGenreName, podcast.Imported.PrimaryGenreName);
            return new UpdateOneModel<PodcastData>(filter, update);
        }

    }
    private async Task<List<PodcastData>> GetExistingPodcasts(IMongoCollection<PodcastData> collection, int[] codes)
    {
        var find = collection
            .Find(x => codes.Contains(x.Code));
            // .Find(Builders<PodcastData>.Filter.In(x => x.Code, codes));
            // .FindAsync(x => codes.Contains(x.Code));
        var list = await find
            .ToListAsync();
        return list;
    }
}