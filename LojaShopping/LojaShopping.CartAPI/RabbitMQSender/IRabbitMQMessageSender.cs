using LojaShopping.Message;

namespace LojaShopping.CartAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
         void SendMessage(BaseMessage baseMessage, string quueName);
    }
}
