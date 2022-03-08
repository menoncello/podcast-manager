using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace PodcastManager.CrossCutting.Rabbit;

public class BaseRabbitEnqueuerAdapter : IDisposable
{
    private IConnection connection = null!;

    public void SetConnection(IConnection connection) =>
        this.connection = connection;

    public void Dispose()
    {
        connection.Dispose();
        GC.SuppressFinalize(this);
    }

    protected void BasicPublish<T>(string queue, T message)
    {
        var (channel, properties) = CreateChannel();
        using (channel)
        {
            var body = PrepareBody();
            channel.BasicPublish(string.Empty, queue, properties, body);
        }

        byte[] PrepareBody()
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
        (IModel, IBasicProperties) CreateChannel()
        {
            var model = connection.CreateModel();
            model.QueueDeclare(queue, true, false, false);
            
            var basicProperties = model.CreateBasicProperties();
            basicProperties.Persistent = true;
            
            return (model, basicProperties);
        }
    }
}
        