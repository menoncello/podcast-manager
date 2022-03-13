namespace PodcastManager.Domain.Models;

public record ImportedPodcast(
    ItunesPodcast? Itunes = null,
    FeedPodcast? Feed = null
);