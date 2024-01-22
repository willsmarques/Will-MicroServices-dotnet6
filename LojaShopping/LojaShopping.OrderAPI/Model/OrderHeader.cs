using LojaShopping.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.OrderAPI.Model
{
    [Table("order_header")]
    public class OrderHeader : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("coupon_code")]
        public string CouponCode { get; set; }

        [Column("valor_final")]
        public Decimal ValorFinal { get; set; }

        [Column("desconto_total")]
        public decimal DescontoTotal { get; set; }

        [Column("primeiro_nome")]
        public string PrimeiroNome { get; set; }

        [Column("ultimo_nome")]
        public string UltimoNome { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("numero_cartao")]
        public string NumeroCartao { get; set; }

        [Column("CVV")]
        public string CVV { get; set; }

        [Column("mes_ano_expiracao")]
        public string MesAnoExpiracao { get; set; }

        [Column("data_compra")]
        public DateTime DateTime { get; set; }

        [Column("data_ordem")]
        public DateTime DataOrdem { get; set; }

        [Column("carr_total_itens")]
        public int CarrTotalItens { get; set; }

        [Column("status_pagamento")]
        public bool StatusPagamento {  get; set; }

        public List<OrderDetail> CarrDetails { get; set; }
    }
}
