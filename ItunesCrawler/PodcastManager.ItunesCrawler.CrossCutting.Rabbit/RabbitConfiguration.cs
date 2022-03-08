namespace PodcastManager.ItunesCrawler.CrossCutting.Rabbit;

public class RabbitConfiguration : PodcastManager.CrossCutting.Rabbit.BaseRabbitConfiguration
{
    public static string ImportAllQueue { get; } =
        Environment.GetEnvironmentVariable("ImportAllQueue")
        ?? "PodcastManager.ImportAll";
    public static string ImportLetterQueue { get; } =
        Environment.GetEnvironmentVariable("ImportLetterQueue")
        ?? "PodcastManager.ImportLetter";
    public static string ImportPageQueue { get; } =
        Environment.GetEnvironmentVariable("ImportPageQueue")
        ?? "PodcastManager.ImportPage";
}