namespace LojaShopping.CartAPI.Data.ValorObjeto;

public class CartVO
{
    public CartHeaderVO CartHeader { get; set; }
    public IEnumerable<CartDetailVO> CartDetails { get; set; }
}
