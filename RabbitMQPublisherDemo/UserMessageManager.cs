using RabbitMQ.Client;

namespace RabbitMQPublisherDemo
{
    public class UserMessageManager
    {
        private static string USER_INBOXES_EXCHANGE = "user-inboxes";

        RabbitMqManager rabbitMqManager = new RabbitMqManager();
        public void OnApplicationStart()
        //{
        //    rabbitMqManager.Call(new ChannelCallable<QueueDeclareOk>()
        //    {
        //        pub
        //    });
        }
    }
}
