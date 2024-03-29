﻿using LojaShopping.OrderAPI.Repositorio;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using LojaShopping.OrderAPI.Model;
using LojaShopping.OrderAPI.Mensagem;
using LojaShopping.OrderAPI.RabbitMQSender;

namespace LojaShopping.OrderAPI.MessagemConsumida
{
    public class RabbitMQPagamentoConsumida : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectFanoutPagamentoExchange";
        private const string PagamentoOrdemAtualizacao = "PagamentoOrdemAtualizacao";

        public RabbitMQPagamentoConsumida(OrderRepository repository)
        {
            _repository = repository;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();

           _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(PagamentoOrdemAtualizacao, false, false, false, null);
            _channel.QueueBind(PagamentoOrdemAtualizacao, ExchangeName, "PagamentoOrdem");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                AtualizacaoPagamentoVO vo = JsonSerializer.Deserialize<AtualizacaoPagamentoVO>(content);
                AtualizarPagamentoStatus(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(PagamentoOrdemAtualizacao, false, consumer);
            return Task.CompletedTask;



        }

        private async Task AtualizarPagamentoStatus(AtualizacaoPagamentoVO vo)
        {
           

         

         
            try
            {
                await _repository.UpdateOrderPagamentoStatus(vo.OrderId,vo.Status);


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
