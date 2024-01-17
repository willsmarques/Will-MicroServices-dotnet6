using AutoMapper;
using LojaShopping.CupomAPI.Data.ValorObjeto;
using LojaShopping.CupomAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaShopping.CupomAPI.Repositorio
{
    public class CupomRepositorio : ICupomRepositorio
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CupomRepositorio(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CupomVO> GetCupomByCupomCod(string codCupom)
        {
           var cupom = await _context.Cupoms.FirstOrDefaultAsync(c => c.CodCupom == codCupom);
           return _mapper.Map<CupomVO>(cupom);
        }
    }
}
