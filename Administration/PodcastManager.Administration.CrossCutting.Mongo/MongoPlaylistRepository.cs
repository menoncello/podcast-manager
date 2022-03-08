using MongoDB.Driver;
using PodcastManager.Administration.CrossCutting.Mongo.Data;
using PodcastManager.Administration.Domain.Repositories;
using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.Domain.Models;

namespace PodcastManager.Administration.CrossCutting.Mongo;

public class MongoPlaylistRepository : MongoRepository, IPlaylistRepository
{
    public async Task<int[]> ListRelatedPodcasts()
    {
        var collection = GetCollection<Playlist>("playlists");
        var pipeline = PipelineDefinition<Playlist, PodcastCodeResult>.Create(
            "{ $unwind: { path: \"$podcastCodes\" } }",
            "{ $group: { _id: 123, codes: { $addToSet: \"$podcastCodes\" } } }");
        
        var agg = collection.Aggregate(pipeline, new AggregateOptions { AllowDiskUse = true });
        var (_, codes) = await agg.SingleAsync();
        return codes;
    }
}