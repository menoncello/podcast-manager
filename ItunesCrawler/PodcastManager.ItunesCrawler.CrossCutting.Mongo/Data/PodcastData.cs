using MongoDB.Bson.Serialization.Attributes;
using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.CrossCutting.Mongo.Data;

[BsonIgnoreExtraElements]
public record PodcastData(ApplePodcast Imported, int Code, string Title, string Feed, string Image,
    bool Published = false)
{
    public static PodcastData FromPodcast(Podcast podcast) =>
        new PodcastData(podcast.Imported, podcast.Code, podcast.Title,
            podcast.Feed, podcast.Image, podcast.Published);
}