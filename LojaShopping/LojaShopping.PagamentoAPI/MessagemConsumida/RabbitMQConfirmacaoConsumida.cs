using LojaShopping.PagamentoAPI.Mensagem;
using LojaShopping.PagamentoAPI.RabbitMQSender;
using LojaShopping.ProcessoPagamento;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace LojaShopping.PagamentoAPI.MessagemConsumida
{
    public class RabbitMQConfirmacaoConsumida : BackgroundService
    {
        private IConnection _connection;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private IModel _channel;
        private readonly IProcessoPagamento _processoPagamento;

        public RabbitMQConfirmacaoConsumida(IProcessoPagamento processoPagamento,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _processoPagamento = processoPagamento;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();

           _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpagamento", false, false, false, arguments: null);
                                          

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PagamentoMensagem vo = JsonSerializer.Deserialize<PagamentoMensagem>(content);
                ProcessPagamento(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpagamento", false, consumer);
            return Task.CompletedTask;



        }

        private async Task ProcessPagamento(PagamentoMensagem vo)
        {
            var result = _processoPagamento.ProcessoPagamento();
            AtualizacaoPagamentoMensagem atualizacaoPagamentoMensagem = new()
            {
                Status = result,
                OrderId = vo.OrderId,
                Email = vo.Email,

            };

            try
            {
                _rabbitMQMessageSender.SendMessage(atualizacaoPagamentoMensagem);


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
