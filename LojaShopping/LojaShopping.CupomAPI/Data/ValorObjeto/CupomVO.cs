using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaShopping.CupomAPI.Data.ValorObjeto
{
    public class CupomVO
    {
        public long Id { get; set; }

        public string CodCupom { get; set; }

        public decimal DescontoTotal { get; set; }
    }
}
