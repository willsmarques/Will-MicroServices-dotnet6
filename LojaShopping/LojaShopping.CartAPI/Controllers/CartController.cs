using LojaShopping.CartAPI.Data.ValorObjeto;
using LojaShopping.CartAPI.Messagens;
using LojaShopping.CartAPI.RabbitMQSender;
using LojaShopping.CartAPI.Repositorio;
using LojaShopping.CartAPI.ValorObjeto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepositorio;
        private ICupomRepositorio _cupomRepositorio;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepositorio, ICupomRepositorio cupomRepositorio, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartRepositorio = cartRepositorio ?? throw new ArgumentNullException(nameof(cartRepositorio));
            _cupomRepositorio = cupomRepositorio ?? throw new ArgumentNullException(nameof(cupomRepositorio));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _cartRepositorio.FindCartByUserId(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _cartRepositorio.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _cartRepositorio.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remover-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _cartRepositorio.RemoveFromCart(id);
            if (!status)
                return BadRequest();

            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            var status = await _cartRepositorio.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status)
                return NotFound();

            return Ok(status);
        }    
        
        [HttpDelete("remove-coupon/{userid}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userid)
        {
            var status = await _cartRepositorio.RemoveCoupon(userid);
            if (!status)
                return NotFound();

            return Ok(status);
        } 
        
        [HttpPost("confirmar")]
        public async Task<ActionResult<ConfirmarHeaderVO>> Confirmar(ConfirmarHeaderVO vo)
        {
            //string token = Request.Headers["Authorization"];
            var token = await HttpContext.GetTokenAsync("access_token");

            if (vo?.UserId == null)
                return BadRequest();

            var cart = await _cartRepositorio.FindCartByUserId(vo.UserId);
            if (cart == null)
                return NotFound();
            if (!string.IsNullOrEmpty(vo.CouponCode))
            {
                CupomVO cupom = await _cupomRepositorio.GetCupom(vo.CouponCode, token);
                if (vo.DescontoTotal != cupom.DescontoTotal)
                {
                    return StatusCode(412);
                }
            }
            vo.CarrDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            //TASK RabbitMQ logic comes here!!!

            _rabbitMQMessageSender.SendMessage(vo, "fila_pagamento");

            await _cartRepositorio.ClearCart(vo.UserId);

            return Ok(vo);
        }
    }
}
