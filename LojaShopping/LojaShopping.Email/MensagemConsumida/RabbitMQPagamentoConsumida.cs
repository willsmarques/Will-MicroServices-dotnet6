using LojaShopping.Email.Mensagem;
using LojaShopping.Email.Repositorio;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace LojaShopping.Email.MessagemConsumida
{
    public class RabbitMQPagamentoConsumida : BackgroundService
    {
        private readonly EmailRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectFanoutPagamentoExchange";
        private const string PagamentoEmailAtualizacaoNome = "PagamentoEmailAtualizacaoNome";


        public RabbitMQPagamentoConsumida(EmailRepository repository)
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
           _channel.QueueDeclare(PagamentoEmailAtualizacaoNome, false, false, false, null);
           _channel.QueueBind(PagamentoEmailAtualizacaoNome, ExchangeName, "PagamentoEmail");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                AtualizacaoPagamentoMensagem mensagem = JsonSerializer.Deserialize<AtualizacaoPagamentoMensagem>(content);
                ProcessamentoLog(mensagem).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(PagamentoEmailAtualizacaoNome, false, consumer);
            return Task.CompletedTask;



        }

        private async Task ProcessamentoLog(AtualizacaoPagamentoMensagem mensagem)
        {
            try
            {
                await _repository.LogEmail(mensagem);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
