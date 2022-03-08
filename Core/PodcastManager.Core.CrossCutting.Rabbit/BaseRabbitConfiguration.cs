namespace PodcastManager.CrossCutting.Rabbit;

public class BaseRabbitConfiguration
{
    public static readonly string Host =
        Environment.GetEnvironmentVariable("RabbitHost")
        ?? "localhost";
}