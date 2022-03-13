namespace PodcastManager.Domain.Models;

public record FullPodcast(
        PodcastStatus? Status,
        ImportedPodcast Imported,
        int Code,
        string Title,
        string Feed,
        string Image,
        bool IsPublished = false)
    : Podcast(Code, Title, Feed, Image, IsPublished);