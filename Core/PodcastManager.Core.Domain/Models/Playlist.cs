using System.Collections.ObjectModel;

namespace PodcastManager.Core.Domain.Models;

public record Playlist(
    string Id,
    string Name,
    string Slug,
    string UserName,
    int[] PodcastCodes,
    string[] Playlists,
    IReadOnlyDictionary<string, PlaylistConfiguration> Config);