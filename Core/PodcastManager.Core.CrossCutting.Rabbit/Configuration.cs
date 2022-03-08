namespace PodcastManager.CrossCutting.Rabbit;

public static class Configuration
{
    public static readonly string Host =
        Environment.GetEnvironmentVariable("RabbitHost")
        ?? "localhost";
}