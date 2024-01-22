using LojaShopping.Message;

namespace LojaShopping.PagamentoAPI.Mensagem
{
    public class AtualizacaoPagamentoMensagem: BaseMessage
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }


    }
}
