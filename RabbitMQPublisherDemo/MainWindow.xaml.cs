using RabbitMQ.Client;
using System.Windows;

namespace RabbitMQPublisherDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string QueueName = "dev-queue-name";
        private static string ExchangeQueue = "dev-exchange-name";

        //private IConnection connection;
        private RabbitMqManager _connectionRabbitMqManager = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var factory = new ConnectionFactory
            {
                VirtualHost = "dev-vhost",
                HostName = "localhost",       
                UserName = "dev-user",
                Password = "Key12345@",
            };
            //connection = factory.CreateConnection();
            //connection.AutoClose = true;
            _connectionRabbitMqManager = new RabbitMqManager(factory);
            _connectionRabbitMqManager.Start();
            var model = _connectionRabbitMqManager.CreateChannel();
            model.ExchangeDeclare(ExchangeQueue, ExchangeType.Direct);
            model.QueueDeclare(QueueName, false, false, false, null);
            model.QueueBind(QueueName, ExchangeQueue, "routingKey",null);
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //connection?.Close();
            _connectionRabbitMqManager.Stop();
        }
    }
}
