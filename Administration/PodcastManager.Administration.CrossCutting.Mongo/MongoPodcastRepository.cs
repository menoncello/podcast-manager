using MongoDB.Driver;
using PodcastManager.Administration.Domain.Repositories;
using PodcastManager.Core.CrossCutting.Mongo;
using PodcastManager.Domain.Models;

namespace PodcastManager.Administration.CrossCutting.Mongo;

public class MongoPodcastRepository : MongoRepository, IPodcastRepository
{
    public async Task<long> PublishPodcasts(int[] codes)
    {
        var collection = GetCollection<FullPodcast>("podcasts");
        var filter = Builders<FullPodcast>.Filter.In(x => x.Code, codes);
        var update = Builders<FullPodcast>.Update.Set(x => x.IsPublished, true)
            .Unset(x => x.Status);
        var result = await collection.UpdateManyAsync(filter, update);
        return result.ModifiedCount;
    }
}