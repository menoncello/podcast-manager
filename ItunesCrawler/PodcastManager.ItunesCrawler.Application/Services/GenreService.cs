using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Domain.Interactors;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public class GenreService : IGenreInteractor
{
    private IItunesAdapter itunes = null!;
    private IEnqueuerAdapter enqueuer = null!;

    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ#";

    public async Task Execute()
    {
        var genres = await itunes.GetGenres();

        var letters = genres
            .SelectMany(_ => Letters, (genre, letter) => new Letter(genre, letter));

        foreach (var letter in letters) enqueuer.EnqueueLetter(letter);
    }

    public void SetItunes(IItunesAdapter itunes)
    {
        this.itunes = itunes;
    }

    public void SetEnqueuer(IEnqueuerAdapter enqueuer)
    {
        this.enqueuer = enqueuer;
    }
}