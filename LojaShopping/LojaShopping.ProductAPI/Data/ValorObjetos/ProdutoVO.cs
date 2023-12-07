using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaShopping.ProductAPI.Data.ValorObjetos;

public class ProdutoVO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal Preco { get; set; }

    public string Descricao { get; set; }

    public string NomeCategoria { get; set; }

    public string ImageURL { get; set; }
}
