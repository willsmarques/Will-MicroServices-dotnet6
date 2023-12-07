using AutoMapper;
using LojaShopping.ProductAPI.Data.ValorObjetos;
using LojaShopping.ProductAPI.Model;
using LojaShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaShopping.ProductAPI.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProdutoRepositorio(MySQLContext context, IMapper mapper)
        {
              _context = context;
            _mapper = mapper;    
        }

        public async Task<IEnumerable<ProdutoVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProdutoVO>>(products);

        }

        public async Task<ProdutoVO> FindById(long id)
        {
            Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProdutoVO>(product);
        }

        public async Task<ProdutoVO> Create(ProdutoVO produto)
        {
           Product product = _mapper.Map<Product>(produto);
           _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProdutoVO>(product);
        }

        public async Task<ProdutoVO> Update(ProdutoVO produto)
        {
            Product product = _mapper.Map<Product>(produto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProdutoVO>(product);
        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {

                Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (product == null) 
                        return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
               
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
