﻿using LojaShopping.Message;
using LojaShopping.PagamentoAPI.Mensagem;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LojaShopping.PagamentoAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "FanoutPagamento";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
         if(ConnectioExiste())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, durable: false);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(
                    exchange: ExchangeName, "", basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<AtualizacaoPagamentoMensagem>((AtualizacaoPagamentoMensagem)message, options);
            
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
            catch (Exception)
            {

            }
        }

        private bool ConnectioExiste()
        {
            if (_connection != null)
                return true;
            CreateConnection();
            return _connection != null;
        }

    }
}
