using LojaShopping.ProductAPI.Data.ValorObjetos;

namespace LojaShopping.ProductAPI.Repositorio;

public interface IProdutoRepositorio
{
    Task<IEnumerable<ProdutoVO>> FindAll();
    
    Task<ProdutoVO> FindById(long id);

    Task<ProdutoVO> Create(ProdutoVO produto);

    Task<ProdutoVO> Update(ProdutoVO produto);

    Task<bool> DeleteById(long id);
}
