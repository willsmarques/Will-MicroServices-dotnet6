using LojaShopping.ProductAPI.Data.ValorObjetos;
using LojaShopping.ProductAPI.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoRepositorio _repositorio;


        public ProdutoController(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new 
                ArgumentNullException(nameof(repositorio));
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<ProdutoVO>>> FindAll()
        {
            var produtos = await _repositorio.FindAll();

            return Ok(produtos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoVO>> FindById(long id)
        {
            var produto = await _repositorio.FindById(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }
    }
}
