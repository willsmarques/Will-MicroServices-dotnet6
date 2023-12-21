using System.ComponentModel.DataAnnotations;

namespace LojaShopping.Web.Models;

public class ProductModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo preço é obrigatório.")]
    public decimal Preco { get; set; }
    
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
    public string NomeCategoria { get; set; }

    [Required(ErrorMessage = "O campo Imagem URL é obrigatório.")]
    public string ImageURL { get; set; }
}

