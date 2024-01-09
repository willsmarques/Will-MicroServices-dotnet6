using LojaShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.CartAPI.Data.ValorObjeto;

public class CartHeaderVO
{
   public long Id { get; set; }
    public string UserId { get; set; }
    public string CouponCode {  get; set; }
}
