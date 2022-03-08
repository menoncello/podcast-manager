using MongoDB.Driver;
using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.ItunesCrawler.CrossCutting.Mongo.Data;
using PodcastManager.ItunesCrawler.Domain.Repositories;
using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.CrossCutting.Mongo;

public class MongoPodcastRepository : MongoRepository, IPodcastRepository
{
    public async Task<(int total, int newPodcasts, int updated)> Upsert(Podcast[] podcasts)
    {
        var collection = GetCollection<PodcastData>("podcasts");

        var (requests, newPodcasts) = await PrepareRequests();

        if (!requests.Any()) return (0, 0, 0);

        var response = await collection.BulkWriteAsync(requests);

        return (podcasts.Length, newPodcasts, (int) response.ModifiedCount);

        UpdateOneModel<PodcastData> CreateUpdateModel(PodcastData podcast)
        {
            var filter = new FilterDefinitionBuilder<PodcastData>().Eq(x => x.Code, podcast.Code);
            var update = CreateUpdate(podcast);
            return new UpdateOneModel<PodcastData>(filter, update);
        }

        UpdateDefinition<PodcastData> CreateUpdate(PodcastData podcast) =>
            Builders<PodcastData>.Update.Set(x => x.Imported.Genres, podcast.Imported.Genres)
                .Set(x => x.Imported.ArtistId, podcast.Imported.ArtistId)
                .Set(x => x.Imported.ArtworkUrl600, podcast.Imported.ArtworkUrl600)
                .Set(x => x.Imported.CollectionExplicitness, podcast.Imported.CollectionExplicitness)
                .Set(x => x.Imported.CollectionId, podcast.Imported.CollectionId)
                .Set(x => x.Imported.CollectionName, podcast.Imported.CollectionName)
                .Set(x => x.Imported.FeedUrl, podcast.Imported.FeedUrl)
                .Set(x => x.Imported.GenreIds, podcast.Imported.GenreIds)
                .Set(x => x.Imported.ContentAdvisoryRating, podcast.Imported.ContentAdvisoryRating)
                .Set(x => x.Imported.PrimaryGenreName, podcast.Imported.PrimaryGenreName);

        async Task<(WriteModel<PodcastData>[] requests, int newPodcasts)> PrepareRequests()
        {
            var codes = podcasts
                .Select(PodcastData.FromPodcast)
                .Select(x => x.Code).ToArray();
            
            var existingPodcasts = await GetExistingPodcasts(codes);
            var existingCodes = existingPodcasts
                .Select(x => x.Code)
                .ToList();

            var result = podcasts
                .Select(PodcastData.FromPodcast)
                .Where(x => !existingCodes.Contains(x.Code))
                .Select(x => (WriteModel<PodcastData>) new InsertOneModel<PodcastData>(x))
                .Concat(existingPodcasts.Select(CreateUpdateModel))
                .ToArray();

            return (result, result.Length - existingCodes.Count);
        }
        
        async Task<List<PodcastData>> GetExistingPodcasts(int[] codes)
        {
            var find = collection
                .Find(x => codes.Contains(x.Code));
            var list = await find
                .ToListAsync();
            return list;
        }
    }
}