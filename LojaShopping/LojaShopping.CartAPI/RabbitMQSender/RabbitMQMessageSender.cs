using LojaShopping.CartAPI.Messagens;
using LojaShopping.Message;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LojaShopping.CartAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string quueName)
        {
            if(ConnectioExiste())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: quueName, false, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: "", routingKey: quueName, basicProperties: null, body: body);

            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<ConfirmarHeaderVO>((ConfirmarHeaderVO)message, options);
            
            return Encoding.UTF8.GetBytes(json);
           
        }


        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _userName,
                };

                _connection = factory.CreateConnection();

            }
            catch(Exception ex)
            {

            }
        }

        private bool ConnectioExiste()
        {
            if (_connection != null)
                return true;
            CreateConnection();
            return false;
        }

    }
}
