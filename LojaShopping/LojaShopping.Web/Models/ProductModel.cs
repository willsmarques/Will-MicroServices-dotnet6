using System.ComponentModel.DataAnnotations;

namespace LojaShopping.Web.Models;

public class ProductModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo preço é obrigatório.")]
    //[DisplayFormat (DataFormatString= "{0:C}", ApplyFormatInEditMode =false) ]
    public decimal Preco { get; set; }
    
    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
    public string NomeCategoria { get; set; }

    [Required(ErrorMessage = "O campo Imagem URL é obrigatório.")]
    public string ImageURL { get; set; }

    [Range(1, 100)]
    public int Count { get; set; } = 1;

    public string SubstringNome()
    {
        if (Name.Length < 24)
            return Name;
        return $"{Name.Substring(0, 21)} ...";
    }

    public string SubstringDescricao()
    {
        if (Descricao.Length < 355)
            return Descricao;
        return $"{Descricao.Substring(0,352)} ...";
    }
}

