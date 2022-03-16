namespace PodcastManager.Feed.Domain.Models;

public record Item(
    string Title,
    DateTime PublicationDate,
    string Guid,
    Enclosure Enclosure,
    int PodcastCode,
    string Podcast = "",
    string[]? Alias = null,
    string? ShortTitle = null,
    string? Link = null,
    string? Description = null,
    string? Subtitle = null,
    string? Summary = null,
    string? Author = null,
    TimeSpan? Duration = null,
    string? Image = null);