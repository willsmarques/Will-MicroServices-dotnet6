using LojaShopping.Message;

namespace LojaShopping.PagamentoAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
         void SendMessage(BaseMessage baseMessage);
    }
}
