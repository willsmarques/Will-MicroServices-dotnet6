using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.CartAPI.Model;

[Table("product")]
public class Product

{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }

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
