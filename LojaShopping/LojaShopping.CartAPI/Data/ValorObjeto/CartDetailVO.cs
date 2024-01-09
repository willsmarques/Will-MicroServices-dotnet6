using LojaShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.CartAPI.Data.ValorObjeto;

public class CartDetailVO 

{
    public long id {  get; set; }
    public long CartHeaderId { get; set; }
    public CartHeaderVO CartHeader { get; set; }
    public long ProductId {  get; set; }
    public ProductVO Product { get; set; }
    public int Count {  get; set; }

}
