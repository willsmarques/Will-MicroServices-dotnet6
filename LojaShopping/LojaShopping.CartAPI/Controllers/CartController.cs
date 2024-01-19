using LojaShopping.CartAPI.Data.ValorObjeto;
using LojaShopping.CartAPI.Messagens;
using LojaShopping.CartAPI.RabbitMQSender;
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
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository repositorio, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repositorio = repositorio;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _repositorio.FindCartByUserId(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _repositorio.SaveOrUpdateCart(vo);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
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

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            var status = await _repositorio.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status)
                return NotFound();

            return Ok(status);
        }    
        
        [HttpDelete("remove-coupon/{userid}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userid)
        {
            var status = await _repositorio.RemoveCoupon(userid);
            if (!status)
                return NotFound();

            return Ok(status);
        } 
        
        [HttpPost("confirmar")]
        public async Task<ActionResult<ConfirmarHeaderVO>> Confirmar(ConfirmarHeaderVO vo)
        {
            if (vo?.UserId == null)
                return BadRequest();

            var cart = await _repositorio.FindCartByUserId(vo.UserId);
            if (cart == null)
                return NotFound();
            vo.CarrDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            //TASK RabbitMQ logic comes here!!!

            _rabbitMQMessageSender.SendMessage(vo, "fila_pagamento");

            return Ok(vo);
        }
    }
}
