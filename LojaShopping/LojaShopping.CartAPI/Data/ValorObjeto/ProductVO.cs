using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.CartAPI.Data.ValorObjeto;


public class ProductVO

{

    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public string NomeCategoria { get; set; }
    public string ImageURL {  get; set; }


}
