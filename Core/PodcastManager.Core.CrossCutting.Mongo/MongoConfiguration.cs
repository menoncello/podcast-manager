namespace PodcastManager.Core.CrossCutting.Mongo;

public static class MongoConfiguration
{
    public static readonly string MongoUrl =
        Environment.GetEnvironmentVariable("MongoUrl")
        ?? "mongodb://127.0.0.1:27017/";
    public static readonly string MongoDatabase =
        Environment.GetEnvironmentVariable("MongoDatabase")
        ?? "podcastManager";
}