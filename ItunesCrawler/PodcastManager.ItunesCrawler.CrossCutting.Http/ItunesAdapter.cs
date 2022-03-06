using HtmlAgilityPack;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;
using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.CrossCutting.Http;

public class ItunesAdapter : IItunesAdapter
{
    private IHttpClientFactory factory = null!;

    public const string GenresUrl = "https://podcasts.apple.com/us/genre/podcasts/id26";
    public const string PageUrl = "https://podcasts.apple.com/us/genre/podcasts-arts-books/id{0}?letter={1}&page={2}";

    public void SetFactory(IHttpClientFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppleGenre[]> GetGenres()
    {
        var client = factory.CreateClient();
        var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, GenresUrl));
        var html = await response.Content.ReadAsStringAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var parent = doc.GetElementbyId("genre-nav");
        var genres = ElementsToGenres(GetLinkElements(parent));

        return genres ?? Array.Empty<AppleGenre>();
        
        AppleGenre[]? ElementsToGenres(IEnumerable<HtmlNode>? elements)
        {
            return elements?
                .Select(e => new AppleGenre(GetUrlId(e), e.InnerText ))
                .ToArray();
        }
    }


    private static IEnumerable<HtmlNode>? GetLinkElements(HtmlNode htmlNode)
    {
        return htmlNode?.Descendants("a")
            .Where(x => x.Attributes.Contains("href"))
            .ToList();
    }

    private static int GetUrlId(HtmlNode e)
    {
        var href = e.Attributes["href"].Value;
        return Convert.ToInt32(href.Split("/id")[1]);
    }

    public async Task<short> GetTotalPages(Letter letter)
    {
        var client = factory.CreateClient();
        return await GetLastPage();

        async Task<short> GetLastPage(short page = 1)
        {
            var url = string.Format(PageUrl, letter.Genre.Id, letter.Char, page);
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var parent = doc.DocumentNode
                .Descendants("ul")
                .FirstOrDefault(x => x.Attributes["class"].Value == "list paginate");

            if (parent == null) return 0;

            var elements = parent.Descendants("a").ToArray();
            var lastPage = short.Parse(elements.Last().InnerText == "Next"
                ? elements[^2].InnerText
                : elements[^1].InnerText);

            return lastPage == page || lastPage < 18
                ? lastPage
                : await GetLastPage(lastPage);
        }
    }

    public async Task<int[]> PodcastsFromPage(Page page)
    {
        var client = factory.CreateClient();
        var ((appleGenre, c), number) = page;
        var url = string.Format(PageUrl, appleGenre.Id, c, number);
        var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var html = await response.Content.ReadAsStringAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var parent = doc.GetElementbyId("selectedcontent");
        
        if (parent == null) return Array.Empty<int>();

        return GetLinkElements(parent)!
            .Select(GetUrlId)
            .ToArray();
    }

    public Task<ApplePodcast[]> GetPodcasts(int[] codes)
    {
        return Task.FromResult(Array.Empty<ApplePodcast>());
    }
}