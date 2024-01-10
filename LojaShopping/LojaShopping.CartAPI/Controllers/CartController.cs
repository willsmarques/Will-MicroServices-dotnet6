using LojaShopping.CartAPI.Data.ValorObjeto;
using LojaShopping.CartAPI.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _repositorio;

        public CartController(ICartRepository repositorio)
        {
            _repositorio = repositorio ?? throw new
                ArgumentNullException(nameof(repositorio));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string userId)
        {
            var cart = await _repositorio.FindCartByUserId(userId);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _repositorio.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart/{id}")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _repositorio.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remover-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _repositorio.RemoveFromCart(id);
            if (!status)
                return BadRequest();

            return Ok(status);
        }
    }
}
