using LojaShopping.OrderAPI.Repositorio;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using LojaShopping.OrderAPI.Model;
using LojaShopping.OrderAPI.Mensagem;
using LojaShopping.OrderAPI.RabbitMQSender;

namespace LojaShopping.OrderAPI.MessagemConsumida
{
    public class RabbitMQConfirmarConsumida : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQConfirmarConsumida(OrderRepository repository,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _repository = repository;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();

           _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "fila_pagamento", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                ConfirmarHeaderVO vo = JsonSerializer.Deserialize<ConfirmarHeaderVO>(content);
                ProcessOrder(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("fila_pagamento", false, consumer);
            return Task.CompletedTask;



        }

        private async Task ProcessOrder(ConfirmarHeaderVO vo)
        {
            OrderHeader order = new()
            {
                UserId = vo.UserId,
                PrimeiroNome = vo.PrimeiroNome,
                UltimoNome = vo.UltimoNome,
                CarrDetails = new List<OrderDetail>(),
                NumeroCartao = vo.NumeroCartao,
                CouponCode = vo.CouponCode,
                CVV = vo.CVV,
                DescontoTotal = vo.DescontoTotal,
                Email = vo.Email,
                MesAnoExpiracao = vo.MesAnoExpiracao,
                DataOrdem = DateTime.Now,
                CarrTotalItens = vo.CarrTotalItens,
                StatusPagamento = false,
                Telefone = vo.Telefone,
                DateTime = vo.DateTime
            };

            foreach (var details in vo.CarrDetails)
            {
                OrderDetail detail = new()
                {
                    ProductId = details.ProductId,
                    NomeProduto = details.Product.Name,
                    Preco = details.Product.Preco,
                    Count = details.Count,
                };
                order.CarrTotalItens += details.Count;
                order.CarrDetails.Add(detail);
            }

            await _repository.AddOrder(order);

            PagamentoVO pagamento = new PagamentoVO
            {
                Name = order.PrimeiroNome + " " + order.UltimoNome,
                CardNumber = order.NumeroCartao,
                MesAnoExpiracao = order.MesAnoExpiracao,
                OrderId = order.Id,
                ValorCompra = order.ValorFinal,
                Email = order.Email
            };
            try
            {
                _rabbitMQMessageSender.SendMessage(pagamento, "orderpagamento");


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
