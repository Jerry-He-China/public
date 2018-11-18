using System;
using RabbitMQ.Client;

namespace RabbitMQPublisherDemo
{
    public class RabbitMqManager
    {
        private readonly ConnectionFactory _factory;
        private IConnection _connection;

        public RabbitMqManager(ConnectionFactory factory)
        {
            _factory = factory;
            _connection = null;
        }

        //public T Call(ChannelCallable<T> callable)
        //{
        //    var channel = CreateChannel();
        //    if (channel == null) return null;
        //    try
        //    {
        //        return callable.Call(channel);
        //    }
        //    finally
        //    {
        //        CloseChannel(channel);
        //    }
        //}

        public IModel CreateChannel()
        {
            return _connection?.CreateModel();
        }

        public void CloseChannel(IModel channel)
        {
            if (channel == null || !channel.IsOpen) return;

            channel.Close();
        }

        public void Start()
        {
            try
            {
                _connection = _factory.CreateConnection();
                _connection.ConnectionShutdown += Connection_ConnectionShutdown;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                WaitAndReconnect();
            }
        }

        public void Stop()
        {
            if (_connection == null)
            {
                return;
            }

            try
            {
                _connection.Close();
            }
#pragma warning disable 168
            catch (Exception ex)
#pragma warning restore 168
            {
                // ignored
            }
            finally
            {
                _connection = null;
            }
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            // reconnect only on unexpected errors
            if (e.Initiator == ShutdownInitiator.Application)
            {
                _connection = null;
                WaitAndReconnect();
            }
        }

        private void WaitAndReconnect()
        {

        }
    }
}
