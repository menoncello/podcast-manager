namespace PodcastManager.FeedUpdater.Domain.Models;

public record Feed(
    string Title,
    string Link,
    string? Description = null,
    string? Language = null,
    Image? Image = null,
    string? Subtitle = null,
    string? Summary = null,
    Owner? Owner = null,
    bool IsEmpty = false)
{
    public string[] Categories { get; init; } = Array.Empty<string>();
    public Item[] Items { get; init; } = Array.Empty<Item>();
    
    public static Feed Empty() => new(string.Empty, string.Empty, IsEmpty: true);
}

public record Item(
    string Title,
    string Link,
    DateTime PublicationDate,
    string Guid,
    Enclosure Enclosure,
    string? Description = null,
    string? Subtitle = null,
    string? Summary = null,
    string? Author = null,
    TimeSpan? Duration = null,
    Image? Image = null
);

public record Enclosure(string Url, int Length, string Type);

public record Owner(string Name, string Email);
public record Image(string Href);
