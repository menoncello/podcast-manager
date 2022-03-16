using MongoDB.Driver;
using PodcastManager.Domain.Models;
using PodcastManager.Feed.CrossCutting.Mongo.Aggregations;
using PodcastManager.Feed.Domain.Models;
using PodcastManager.Feed.Domain.Repositories;

namespace PodcastManager.Feed.CrossCutting.Mongo;

public class MongoPlaylistRepository : IPlaylistRepository
{
    private IMongoDatabase database = null!;

    public async Task<Domain.Models.Feed> GetFeed(string username, string slug)
    {
        var collection = database.GetCollection<Playlist>("playlists");
        var pipeline = PipelineDefinition<Playlist, Item>
            .Create(AggregationFactory.Instance.GetSinglePlaylistFeed(username, slug, 500));
        var cursor = await collection.AggregateAsync(pipeline);

        var feed = new Domain.Models.Feed($"Feed {slug}")
        {
            Items = (await cursor.ToListAsync()).ToArray()
        };
        
        return feed;
    }

    public void SetDatabase(IMongoDatabase database) =>
        this.database = database;
}