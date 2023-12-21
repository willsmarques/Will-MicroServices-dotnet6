using LojaShopping.ProductAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.ProductAPI.Model;

[Table("product")]
public class Product : BaseEntity

{
    [Column("name")]
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Column("Preco")]
    [Required]
    [Range(1,10000)]
    public decimal Preco { get; set; }

    [Column ("descricao")]
    [StringLength(500)]
    public string Descricao { get; set; }

    [Column("nome_categoria")]
    [StringLength (50)]
    public string NomeCategoria { get; set; }

    [Column("imagem_url")]
    [StringLength(300)]
    public string ImageURL {  get; set; }


}
