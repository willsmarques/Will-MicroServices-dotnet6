using LojaShopping.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.OrderAPI.Model
{
    [Table("order_detail")]
    public class OrderDetail :BaseEntity
    {
        public long OrderHeaderId { get; set; }
        
        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        [Column("ProductId")]
        public long ProductId {  get; set; }

        [Column("count")]
        public int Count {  get; set; }

        [Column("nome_produto")]
        public string NomeProduto {  get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

    }
}
