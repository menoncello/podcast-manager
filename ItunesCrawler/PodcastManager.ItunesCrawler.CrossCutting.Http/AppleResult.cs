using PodcastManager.ItunesCrawler.Models;

namespace PodcastManager.ItunesCrawler.CrossCutting.Http;

public record AppleResult(ApplePodcast[] Results);