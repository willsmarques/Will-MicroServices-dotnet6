using LojaShopping.Message;

namespace LojaShopping.OrderAPI.Mensagem
{
    public class PagamentoVO : BaseMessage
    {
        public long OrderId {  get; set; }
        public string Name {  get; set; }
        public string CardNumber { get; set;}
        public string CVV { get; set;}
        public string MesAnoExpiracao {  get; set;}
        public string Email { get; set;}
        public decimal ValorCompra {  get; set;}
        
    }
}
