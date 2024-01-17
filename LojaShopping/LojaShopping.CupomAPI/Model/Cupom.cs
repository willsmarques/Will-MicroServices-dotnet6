using LojaShopping.CupomAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaShopping.CupomAPI.Model;

[Table("cupom")]
public class Cupom : BaseEntity

{

    [Column("Cod_Cupom")]
    [Required]
    [StringLength(30)]
    public string CodCupom { get; set; }

    [Column("Desconto_Total")]
    [Required]
    public decimal DescontoTotal { get; set; }
}
