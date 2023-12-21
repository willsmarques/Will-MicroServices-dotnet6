using LojaShopping.ProductAPI.Data.ValorObjetos;
using LojaShopping.ProductAPI.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace LojaShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoRepositorio _repositorio;

        [HttpPost]
        public async Task<ActionResult<ProdutoVO>> Create([FromBody] ProdutoVO vo)
        {
            if (vo == null)
                return BadRequest();
            var produto = await _repositorio.Create(vo);
            return Ok(produto);
        }

       [HttpPut]
       public async Task<ActionResult<ProdutoVO>> Update([FromBody]  ProdutoVO vo)
        {
           if(vo == null)
                return BadRequest();
            var produto = await _repositorio.Update(vo);
            return Ok(produto);
        }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repositorio.DeleteById(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
