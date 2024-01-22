using LojaShopping.CupomAPI.Data.ValorObjeto;
using LojaShopping.CupomAPI.Repositorio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaShopping.CupomAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CupomController : ControllerBase
    {
        private ICupomRepositorio _repositorio;

        public CupomController(ICupomRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new
                ArgumentNullException(nameof(repositorio));
        }

        [HttpGet("{codCupom}")]
        [Authorize]
        public async Task<ActionResult<CupomVO>> GetCupomByCupomCod(string codCupom)
        {
           // var token = await HttpContext.GetTokenAsync("acess_token");
            var cupom = await _repositorio.GetCupomByCupomCod(codCupom);
            if (cupom == null)
                return NotFound();

            return Ok(cupom);
        }

    }
}