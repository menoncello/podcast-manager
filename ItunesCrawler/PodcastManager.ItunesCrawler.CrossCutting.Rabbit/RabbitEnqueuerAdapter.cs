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
    
    public void EnqueueLetter(Letter letter) =>
        BasicPublish(Configuration.ImportLetterQueue, letter);

    public void EnqueuePage(Page page) =>
        BasicPublish(Configuration.ImportPageQueue, page);

    public void Dispose() => connection.Dispose();

    private void BasicPublish<T>(string queue, T message)
    {
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue, true, false, autoDelete: false);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        channel.BasicPublish(string.Empty, queue, properties, body);
    }
}