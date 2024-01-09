using LojaShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.CartAPI.Model
{
    [Table("cart_detail")]
    public class CartDetail :BaseEntity
    {
        public long CartHeaderId { get; set; }

        [ForeignKey("CarHeaderId")]
        public virtual CartHeader CartHeader { get; set; }

        public long ProductId {  get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("count")]
        public int Count {  get; set; }

    }
}
