namespace LojaShopping.Web.Models;

public class CartHeaderViewModel
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string CouponCode {  get; set; }
    public Decimal ValorFinal {  get; set; }

    public decimal DescontoTotal {  get; set; }
    public string PrimeiroNome {  get; set; }
    public string UltimoNome { get; set; }
    public string Telefone {  get; set; }
    public string Email {  get; set; }
    public string NumeroCartao {  get; set; }
    public string CVV {  get; set; }
    public string MesAnoExpiracao {  get; set; }
    public DateTime DateTime { get; set; }
}
