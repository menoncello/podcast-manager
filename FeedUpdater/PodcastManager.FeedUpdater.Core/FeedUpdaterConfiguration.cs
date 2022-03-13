namespace PodcastManager.FeedUpdater;

public static class FeedUpdaterConfiguration
{
    public static readonly TimeSpan PublishedPodcastNextSchedule =
        TimeSpan.Parse(Environment.GetEnvironmentVariable("PublishedPodcastNextSchedule")
                       ?? "00:30:00");
    public static readonly TimeSpan PodcastNextSchedule =
        TimeSpan.Parse(Environment.GetEnvironmentVariable("PodcastNextSchedule")
                       ?? "03:00:00");
}