namespace PodcastManager.ItunesCrawler.CrossCutting.Mongo;

public static class Configuration
{
    public static string MongoUrl =
        Environment.GetEnvironmentVariable("MongoUrl")
        ?? "mongodb://127.0.0.1:27017/";
}