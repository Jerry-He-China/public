using System.Runtime.Remoting.Channels;
using RabbitMQ.Client;

namespace RabbitMQPublisherDemo
{
    public interface ChannelCallable<T>
    {
        string Description { get; set; }

        T Call(IModel channel);
    }
}
