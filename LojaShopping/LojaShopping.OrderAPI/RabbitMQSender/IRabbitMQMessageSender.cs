using LojaShopping.Message;

namespace LojaShopping.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
         void SendMessage(BaseMessage baseMessage, string quueName);
    }
}
