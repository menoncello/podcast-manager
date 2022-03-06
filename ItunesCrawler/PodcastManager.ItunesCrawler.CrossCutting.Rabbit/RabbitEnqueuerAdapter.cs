using System.Text;
using Newtonsoft.Json;
using PodcastManager.ItunesCrawler.Adapters;
using PodcastManager.ItunesCrawler.Messages;
using RabbitMQ.Client;

namespace PodcastManager.ItunesCrawler.CrossCutting.Rabbit;

public class RabbitEnqueuerAdapter : IEnqueuerAdapter, IDisposable
{
    private IConnection connection = null!;

    public void SetConnection(IConnection connection) =>
        this.connection = connection;
    
    public Task EnqueueLetter(Letter letter) =>
        BasicPublishAsync(Configuration.ImportLetterQueue, letter);

    public Task EnqueuePage(Page page) =>
        BasicPublishAsync(Configuration.ImportPageQueue, page);

    public void Dispose() => connection.Dispose();

    private Task BasicPublishAsync<T>(string queue, T message) =>
        new(() => BasicPublish(queue, message));

    private void BasicPublish<T>(string queue, T message)
    {
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(string.Empty, queue, null, body);
    }
}