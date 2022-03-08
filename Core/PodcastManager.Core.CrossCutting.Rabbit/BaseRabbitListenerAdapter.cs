using System.Text;
using Newtonsoft.Json;
using PodcastManager.Adapters;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PodcastManager.CrossCutting.Rabbit;

public abstract class BaseRabbitListenerAdapter : IListenerAdapter, IDisposable
{
    private IConnection connection = null!;
    private IModel channel = null!;

    public void SetConnectionFactory(IConnectionFactory connectionFactory)
    {
        connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
    }

    public abstract void Listen();

    public void Dispose()
    {
        connection.Dispose();
        channel.Dispose();
        GC.SuppressFinalize(this);
    }
    
    protected void ListenTo<T>(string queue, Func<T, Task> action)
    {
        Console.WriteLine($"{DateTime.Now} - listening to: {queue}");
        ConfigureChannel();
        channel.BasicConsume(queue, false, ConfigureConsumer());

        async Task TryProcessMessage(BasicDeliverEventArgs basicDeliverEventArgs)
        {
            try
            {
                await ProcessMessage(basicDeliverEventArgs);
                channel.BasicAck(basicDeliverEventArgs.DeliveryTag, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                channel.BasicNack(basicDeliverEventArgs.DeliveryTag, false, false);
            }
        }
        async Task ProcessMessage(BasicDeliverEventArgs args)
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());
            if (string.IsNullOrEmpty(json)) json = "{}";
            
            var message = JsonConvert.DeserializeObject<T>(json);
            
            Console.WriteLine($"{DateTime.Now} - Message Received: {message}");
            await action(message!);
        }
        void ConfigureChannel()
        {
            channel.QueueDeclare(queue, true, false, false);
            channel.BasicQos(0, 5, true);
        }
        EventingBasicConsumer ConfigureConsumer()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, args) => await TryProcessMessage(args);
            return consumer;
        }
    }
}