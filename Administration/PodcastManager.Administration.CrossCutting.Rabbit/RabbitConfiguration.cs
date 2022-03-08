using PodcastManager.CrossCutting.Rabbit;

namespace PodcastManager.Administration.CrossCutting.Rabbit;

public class RabbitConfiguration : BaseRabbitConfiguration
{
    public static string ImportAllQueue { get; } =
        Environment.GetEnvironmentVariable("PublishAllFromPlaylists")
        ?? "PodcastManager.PublishAllFromPlaylists";
}