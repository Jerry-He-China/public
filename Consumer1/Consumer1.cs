using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer1
{
    /// <summary>
    /// using custom exchange to communicate
    /// </summary>
    class Consumer1
    {
        static void Main(string[] args)
        {
            var factory = Constant.GetConnectionFactory();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: Constant.exchangeName1, type: Constant.exchangeDirectType,
                        durable: false, autoDelete: true, arguments: null);
                    channel.QueueDeclare(queue: Constant.queueName1, durable: false, autoDelete: true, exclusive: false,
                        arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        var body = Encoding.UTF8.GetString(ea.Body);
                        var random = new Random();
                        var d = random.Next(10);
                        Thread.Sleep(d * 1000);
                        Console.WriteLine($"[x] Received {body}");
                    };

                    channel.BasicConsume(queue:Constant.queueName1, noAck:true, consumer:consumer);
                    Console.WriteLine("Press any Key to exit.");
                    Console.ReadKey();
                }
            }
        }
    }
}
