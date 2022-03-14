namespace PodcastManager.Feed.Adapters;

public interface IFeedAdapter
{
    string Build(Domain.Models.Feed feed);
}