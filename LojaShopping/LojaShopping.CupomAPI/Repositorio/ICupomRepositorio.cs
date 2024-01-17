using LojaShopping.CupomAPI.Data.ValorObjeto;

namespace LojaShopping.CupomAPI.Repositorio
{
    public interface ICupomRepositorio
    {
        Task<CupomVO> GetCupomByCupomCod(string codCupom);
    }
}
