namespace PodcastManager.Domain.Models;

public record PlaylistConfiguration(
    string? Name,
    int? EpisodeCount,
    DateOnly? StartDate,
    SkipTime? Skip);