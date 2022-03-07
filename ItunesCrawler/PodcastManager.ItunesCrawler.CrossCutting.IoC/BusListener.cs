using System.Text;
using Newtonsoft.Json;
using PodcastManager.ItunesCrawler.Domain.Factories;
using PodcastManager.ItunesCrawler.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PodcastManager.ItunesCrawler.CrossCutting.IoC;

public class BusListener : IDisposable
{
    private readonly IInteractorFactory interactorFactory;
    private readonly IConnection connection;
    private readonly IModel channel;
    public BusListener(IInteractorFactory interactorFactory)
    {
        this.interactorFactory = interactorFactory;

        var connectionFactory = new ConnectionFactory { HostName = Rabbit.Configuration.Host };
        connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
    }

    public void Listen()
    {
        ListenTo<ImportAll>(Rabbit.Configuration.ImportAllQueue,
            _ => interactorFactory.CreateGenre().Execute());
        ListenTo<Letter>(Rabbit.Configuration.ImportLetterQueue, 
            interactorFactory.CreateLetter().Execute);
        ListenTo<Page>(Rabbit.Configuration.ImportPageQueue,
            interactorFactory.CreatePage().Execute);
    }

    private void ListenTo<T>(string queue, Func<T, Task> action)
    {
        channel.QueueDeclare(queue, true, false, false);
        channel.BasicQos(0, 5, true);
        Console.WriteLine($"{DateTime.Now} - listening to: {queue}");
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (_, args) =>
        {
            try
            {
                var body = args.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                if (string.IsNullOrEmpty(json)) json = "{}";

                var message = JsonConvert.DeserializeObject<T>(json);
                Console.WriteLine($"{DateTime.Now} - Message Received: {message}");
                await action(message!);
                channel.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                channel.BasicNack(args.DeliveryTag, false, false);
                // throw;
            }
        };
        channel.BasicConsume(queue, false, consumer);
    }

    public void Dispose()
    {
        connection.Dispose();
        channel.Dispose();
    }
}