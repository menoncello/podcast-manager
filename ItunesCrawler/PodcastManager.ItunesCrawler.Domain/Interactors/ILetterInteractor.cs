using System.Threading.Tasks;
using PodcastManager.ItunesCrawler.Messages;

namespace PodcastManager.ItunesCrawler.Application.Services;

public interface ILetterInteractor
{
    Task Execute(Letter letter);
}