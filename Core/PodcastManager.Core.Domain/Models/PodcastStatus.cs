namespace PodcastManager.Domain.Models;

public record PodcastStatus(
    DateTime NextUpdate,
    int? TotalEpisodes = 0,
    DateTime? LastTimeUpdated = null,
    int ErrorCount = 0,
    string[]? Errors = null
);