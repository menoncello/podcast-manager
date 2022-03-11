using PodcastManager.Adapters;

namespace PodcastManager.CrossCutting.System.Adapters;

public class UtcDateTimeAdapter : IDateTimeAdapter
{
    public DateTime Now() => DateTime.UtcNow;
}