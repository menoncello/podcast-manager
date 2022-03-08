namespace PodcastManager.Domain.Models;

public record Podcast(
    int Code,
    string Title,
    string Feed,
    string Image,
    bool IsPublished = false);