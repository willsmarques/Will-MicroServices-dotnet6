using LojaShopping.CartAPI.ValorObjeto;

namespace LojaShopping.CartAPI.Repositorio
{
    public interface ICupomRepositorio
    {
        Task<CupomVO> GetCupom(string codCupom,string token);
    }
}
