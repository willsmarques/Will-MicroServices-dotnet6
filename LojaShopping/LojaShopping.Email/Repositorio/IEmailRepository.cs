using LojaShopping.Email.Mensagem;

namespace LojaShopping.Email.Repositorio
{
    public interface IEmailRepository
    {
        Task LogEmail(AtualizacaoPagamentoMensagem menssage);

    }
}
