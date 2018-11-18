using RabbitMQ.Client;

namespace Common
{
    public class Constant
    {
        public const string hostName = "localhost";
        public const string virtualHost = "dev-vhost";

        public const string user = "dev-user";
        public const string password = "Key12345@";

        public const string queueName0 = "myqueue0";
        public const string queueName1 = "myqueue1";

        public const string exchangeName0 = "myexchange0";
        public const string exchangeName1 = "myexchange1";
        public const string exchangeName2 = "myexchange2";

        public const string exchangeDirectType = "direct";
        public const string exchangeFanoutType = "fanout";
        public const string exchangeHeadersType = "headers";
        public const string exchangeTopicType = "topic";

        public const string message = "hello,world";

        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = hostName,
                VirtualHost = virtualHost,
                UserName = user,
                Password = password
            };
        }
    }
}
