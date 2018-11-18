using System;
using System.Text;
using Common;
using RabbitMQ.Client;

namespace Producer1
{
    class Producer1
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

                    channel.QueueDeclare(queue: Constant.queueName1, durable: false, exclusive: false, autoDelete: true,
                        arguments: null);

                    channel.QueueBind(queue: Constant.queueName1, exchange: Constant.exchangeName1,
                        routingKey: Constant.queueName1, arguments: null);

                    var body = Encoding.UTF8.GetBytes(Constant.message);
                    channel.BasicPublish(exchange: Constant.exchangeName1, routingKey: Constant.queueName1, basicProperties: null,
                        body: body);

                    Console.WriteLine($"Send {Constant.message}");
                }
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
