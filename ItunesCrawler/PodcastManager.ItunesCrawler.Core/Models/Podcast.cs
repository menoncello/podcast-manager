using PodcastManager.Domain.Models;

namespace PodcastManager.ItunesCrawler.Models;

public record Podcast(ApplePodcast Imported, int Code, string Title, string Feed, string Image,
    bool IsPublished = false)
{
    public static Podcast FromApple(ApplePodcast imported)
    {
        return new Podcast(imported, imported.CollectionId, imported.CollectionName,
            imported.FeedUrl, imported.ArtworkUrl600);
    }
}