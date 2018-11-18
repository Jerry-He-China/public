using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    /// <summary>
    /// Using default exchange and queue to communicate
    /// </summary>
    class Producer0
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
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true,
                        arguments: null);

                    var message = "Hello,world";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange:"",routingKey:queueName, basicProperties:null,body:body);

                    Console.WriteLine($"Send {message}");
                }
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
