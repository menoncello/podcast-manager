using System.Diagnostics.CodeAnalysis;

namespace PodcastManager.Administration.CrossCutting.Mongo.Data;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public record PodcastCodeResult(
    int _id,
    int[] codes);
