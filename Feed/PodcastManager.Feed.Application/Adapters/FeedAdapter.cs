using System.Security;
using System.Text;
using PodcastManager.Feed.Adapters;

namespace PodcastManager.Feed.Application.Adapters;

public class FeedAdapter : IFeedAdapter
{
    public string Build(Domain.Models.Feed feed)
    {
        var sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
        
        Process(feed, sb);

        return sb.ToString();
    }

    private static void Process(Domain.Models.Feed feed, StringBuilder sb)
    {
        using (new Rss(sb))
        using (new Channel(sb))
        {
            AddTag(sb, "title", SecurityElement.Escape(feed.Title), 2);
            AddTag(sb, "link", SecurityElement.Escape(feed.Link), 2);
            AddTag(sb, "description", SecurityElement.Escape(feed.Description), 2);

            if (feed.Image != null)
                using (new ImageTag(sb))
                {
                    AddTag(sb, "url", SecurityElement.Escape(feed.Image?.Url), 3);
                    AddTag(sb, "title", SecurityElement.Escape(feed.Image?.Title), 3);
                    AddTag(sb, "link", SecurityElement.Escape(feed.Image?.Link), 3);
                }

            foreach (var item in feed.Items)
            {
                using (new ItemTag(sb))
                {
                    AddTag(sb, "title", SecurityElement.Escape(item.Title), 3);
                    AddTag(sb, "pubDate", item.PublicationDate.ToString("R").Replace("GMT", "+0000"), 3);
                    AddTagAutoClose(sb, "enclosure", 3,
                        ("url", SecurityElement.Escape(item.Enclosure.url)),
                        ("length", item.Enclosure.length.ToString()),
                        ("type", item.Enclosure.type));
                    AddTag(sb, "guid", SecurityElement.Escape(item.Guid), 3, ("isPermalink", "false"));
                }
            }
        }
    }

    private class ItemTag : IDisposable
    {
        private readonly StringBuilder sb;

        public ItemTag(StringBuilder sb)
        {
            this.sb = sb;
            sb.AppendLine("        <item>");
        }

        public void Dispose()
        {
            sb.AppendLine("        </item>");
        }
    }

    private class ImageTag : IDisposable
    {
        private readonly StringBuilder sb;

        public ImageTag(StringBuilder sb)
        {
            this.sb = sb;
            sb.AppendLine("        <image>");
        }

        public void Dispose()
        {
            sb.AppendLine("        </image>");
        }
    }

    private static void AddTagAutoClose(StringBuilder sb, string tag, int level, params (string, string)[] attributes)
    {
        sb.Append($"{new string(' ', level * 4)}<{tag}");

        foreach (var (attr, value) in attributes)
            sb.Append($" {attr}=\"{value}\"");

        sb.AppendLine(" />");
    }

    private static void AddTag(StringBuilder sb, string tag,
        string? value, ushort level, params (string, string)[] attributes)
    {
        if (string.IsNullOrWhiteSpace(value)) return;
        sb.Append($"{new string(' ', level * 4)}<{tag}");
        
        foreach (var (attr, attrValue) in attributes)
            sb.Append($" {attr}=\"{attrValue}\"");

        sb.AppendLine($">{value}</{tag}>");
    }

    private class Channel : IDisposable
    {
        private readonly StringBuilder sb;

        public Channel(StringBuilder sb)
        {
            this.sb = sb;
            sb.AppendLine("    <channel>");
        }

        public void Dispose()
        {
            sb.AppendLine("    </channel>");
        }
    }

    private class Rss : IDisposable
    {
        private readonly StringBuilder sb;

        public Rss(StringBuilder sb)
        {
            this.sb = sb;
            sb.AppendLine("<rss>");
        }

        public void Dispose()
        {
            sb.Append("</rss>");
        }
    }
}