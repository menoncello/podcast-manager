using PodcastManager.Adapters;

namespace PodcastManager.Doubles;

public class DateTimeStub : IDateTimeAdapter
{
    public DateTime Now() => new(2020, 6, 25, 10, 0, 0);
}