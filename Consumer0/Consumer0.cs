using System;
using System.IO;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer0
{
    /// <summary>
    /// Using default exchange and queue to communicate
    /// </summary>
    class Consumer0
    {
        private const string queueName = "myqueue0";
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                VirtualHost = "dev-vhost",
                UserName = "dev-user",
                Password = "Key12345@"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: true,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body;
                        //var ms = new MemoryStream(body.Length);
                        
                        Console.WriteLine($"[X] Received {Encoding.UTF8.GetString(ea.Body)}");
                    };

                    channel.BasicConsume(queue:"",noAck:true,consumer:consumer);

                    Console.WriteLine("Press any key to exit.");

                    Console.Read();
                }
            }

        }
    }
}
