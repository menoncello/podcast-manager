using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class LetterService : ILetterInteractor
{
    private IEnqueuerAdapter enqueuer = null!;
    private IItunesAdapter itunes = null!;

    public async Task Execute(Letter letter)
    {
        var totalPages = await itunes.GetTotalPages(letter);
        for (short i = 1; i < totalPages + 1; i++)
            await enqueuer.EnqueuePage(new Page(letter, i));
    }

    public void SetEnqueuer(IEnqueuerAdapter enqueuer)
    {
        this.enqueuer = enqueuer;
    }

    public void SetItunes(IItunesAdapter itunes)
    {
        this.itunes = itunes;
    }
}