using MongoDB.Driver;

namespace PodcastManager.Feed.CrossCutting.Mongo.Aggregations;

public class AggregationFactory
{
    // private readonly string[] singlePlaylistFeed;
    public static AggregationFactory Instance { get; } = new();
    private AggregationFactory()
    {
    //     singlePlaylistFeed = File
    //         .ReadAllText(@"./Aggregations/SinglePlaylistFeed.json")
    //         .Split("\n\n");
    }

    public IEnumerable<string> GetSinglePlaylistFeed(string username, string slug, int limit)
    {
        return new List<string>
        {
            $"{{ $match: {{ slug: \"{slug}\", username: \"{username}\" }} }}",
            "{ $lookup: { from: \"podcasts\", localField: \"podcastCodes\", foreignField: \"code\", as: \"podcast\" } }",
            "{ $unwind: \"$podcast\" }",
            "{ $lookup: { from: \"episodes\", localField: \"podcast.code\", foreignField: \"podcastCode\", as: \"episode\" } }",
            "{ $unwind: \"$episode\" }",
            "{ $project: { _id: 0, " +
            "PodcastCode: \"$podcast.code\", " +
            "Alias: \"$podcast.alias\", " +
            "ShortTitle: \"$podcast.shortTitle\", " +
            "Podcast: \"$podcast.title\", " +
            "Author: \"$episode.author\", " +
            "Duration: \"$episode.duration\", " +
            "Title: \"$episode.title\", " +
            "Link: \"$episode.link\", " +
            "Guid: { $concat: [\"$episode.guid\", \"?podcastCode=\", { $toString: \"$podcast.code\" }] }, " +
            "Subtitle: \"$episode.subtitle\", " +
            "Description: \"$episode.description\", " +
            "Summary: \"$episode.summary\", " +
            "Image: \"$episode.image.href\", " +
            "Enclosure: \"$episode.enclosure\", " +
            "PublicationDate: \"$episode.publicationDate\" } }",
            // "",
            "{ $sort: { PublicationDate: -1 } }",
            $"{{ $limit: {limit} }}",
        };
    }
}