using System.Xml.Linq;
using PodcastManager.FeedUpdater.Domain.Interactors;
using PodcastManager.FeedUpdater.Domain.Models;

namespace PodcastManager.FeedUpdater.Application.Services;

public class FeedConverterService : IFeedConverterInteractor
{
    public Feed Execute(string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return Feed.Empty();
        
        var rss = XElement.Parse(data);
        
        var channel = rss.Element("channel");

        if (channel == null) throw new ChannelNotFoundException();


        var feed = new Feed(
                GetElementValue(channel, "title"),
                GetElementValue(channel, "link"),
                GetElementNullableValue(channel, "description"),
                GetElementNullableValue(channel, "language"),
                GetElementNullableValue(channel, "itunes:image", ConvertToImage),
                GetElementNullableValue(channel, "itunes:subtitle"),
                GetElementNullableValue(channel, "itunes:summary"),
                GetElementNullableValue(channel, "itunes:owner", ConvertToOwner)
        )
        {
            Categories = GetElementValues(channel, "itunes:category", ConvertToCategory),
            Items = GetElementValues(channel, "item", ConvertToItem)
        };
        
        return feed;
    }

    private Item ConvertToItem(XElement element)
    {
        return new Item(
            GetElementValue(element, "title"),
            GetElementValue(element, "link"),
            GetElementValue(element, "pubDate", x => ConvertToDate(x.Value)),
            GetElementValue(element, "guid"),
            GetElementValue(element, "enclosure", ConvertToEnclosure),
            GetElementValue(element, "description"),
            GetElementValue(element, "itunes:subtitle"),
            GetElementValue(element, "itunes:summary"),
            GetElementValue(element, "itunes:author"),
            GetElementValue(element, "itunes:duration", ConvertToTimeSpan),
            GetElementValue(element, "itunes:image", ConvertToImage)
        );
    }

    private TimeSpan ConvertToTimeSpan(XElement element)
    {
        var numbers = element.Value
            .Split(':')
            .Select(int.Parse)
            .ToArray();

        return numbers.Length switch
        {
            >= 3 => new TimeSpan(numbers[^2], numbers[^1], numbers[^0]),
            2 => new TimeSpan(0, numbers[0], numbers[1]),
            _ => new TimeSpan(0, 0, numbers[0])
        };
    }

    private Enclosure ConvertToEnclosure(XElement element) =>
        new(
            GetAttributeValue(element, "url"),
            int.Parse(GetAttributeValue(element, "length")),
            GetAttributeValue(element, "type"));

    public DateTime ConvertToDate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(text));

        if (DateTime.TryParse(text, out var date))
            return date.ToUniversalTime();

        throw new DateTimeBadFormattedException(text);
    }

    private static string ConvertToCategory(XElement element) =>
        GetAttributeValue(element, "text");

    private static string GetAttributeValue(XElement element, string attributeName)
    {
        var attribute = element.Attribute(attributeName);
        if (attribute == null) throw new HrefAttributeNotFoundInImageException();
        return attribute.Value;
    }

    private static Image ConvertToImage(XElement element) =>
        new(GetAttributeValue(element, "href"));

    private static Owner ConvertToOwner(XElement element)
    {
        return new Owner(
            GetElementValue(element, "itunes:name"),
            GetElementValue(element, "itunes:email")
        );
    }

    private static string? GetElementNullableValue(XContainer channel, string elementName) => 
        channel.Element(GetXName(elementName))?.Value;
    private static string[] GetElementNullableValues(XContainer channel, string elementName) =>
        channel.Elements(GetXName(elementName))
            .Select(x => x.Value)
            .ToArray();

    private static T? GetElementNullableValue<T>(XContainer channel, string elementName, Func<XElement, T> converter)
    {
        var element = channel.Element(GetXName(elementName));
        return element == null ? default : converter(element);
    }
    private static string GetElementValue(XContainer channel, string elementName)
    {
        var element = channel.Element(GetXName(elementName));
        if (element == null) throw new PropertyNotExistsException(elementName);
        return element.Value;
    }
    private static string[] GetElementValues(XContainer channel, string elementName)
    {
        var elements = channel.Elements(GetXName(elementName));
        if (elements == null) throw new PropertyNotExistsException(elementName);
        return elements
            .Select(x => x.Value)
            .ToArray();
    }

    private static T[] GetElementValues<T>(XContainer channel, string elementName, Func<XElement, T> converter)
    {
        var elements = channel.Elements(GetXName(elementName));
        if (elements == null) throw new PropertyNotExistsException(elementName);
        return elements
            .Select(converter)
            .ToArray();
    }

    private static T GetElementValue<T>(XContainer channel, string elementName, Func<XElement, T> converter)
    {
        var element = channel.Element(GetXName(elementName));
        if (element == null) throw new PropertyNotExistsException(elementName);
        return converter(element);
    }

    private static XName GetXName(string elementName)
    {
        if (!elementName.Contains(':'))
            return elementName;

        var items = elementName.Split(':');
        var ns = Namespaces[items[0]];
        return XName.Get(items[1], ns);
    }

    private static readonly Dictionary<string, string> Namespaces = new()
    {
        {"itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd"}
    };
}

public class DateTimeBadFormattedException : Exception
{
    public string BadFormattedDateTime { get; }

    public DateTimeBadFormattedException(string badFormattedDateTime)
    {
        BadFormattedDateTime = badFormattedDateTime;
    }
}

public class HrefAttributeNotFoundInImageException : Exception
{
}

public class PropertyNotExistsException : Exception
{
    public string Title { get; }

    public PropertyNotExistsException(string title)
    {
        Title = title;
    }
}

public class ChannelNotFoundException : Exception
{
}