using PodcastManager.CrossCutting.Rabbit;

namespace PodcastManager.FeedUpdater.CrossCutting.Rabbit;

public class UpdaterRabbitConfiguration : BaseRabbitEnqueuerAdapter
{
    public static string UpdatePodcastQueue { get; } =
        Environment.GetEnvironmentVariable("UpdatePodcast")
        ?? "PodcastManager.UpdatePodcast";
    public static string UpdatePodcastsQueue { get; } =
        Environment.GetEnvironmentVariable("UpdatePodcasts")
        ?? "PodcastManager.UpdatePodcasts";
    public static string UpdatePublishedPodcastsQueue { get; } =
        Environment.GetEnvironmentVariable("UpdatePublishedPodcasts")
        ?? "PodcastManager.UpdatePublishedPodcasts";
}