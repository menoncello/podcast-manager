using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.Core.Domain.Models;

public record Podcast(int Code, string Title, string Feed, string Image,
    bool Published = false);
public record FullPodcast(
    PodcastStatus Status,
    ApplePodcast Imported,
    int Code,
    string Title,
    string Feed,
    string Image,
    bool Published = false)
    : Podcast(Code, Title, Feed, Image, Published);

public record PodcastStatus
{
}