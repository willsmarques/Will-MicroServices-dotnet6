using LojaShopping.Web.Models;
using LojaShopping.Web.Services.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponSerevice _couponSerevice;

        public CartController(IProductService productService,
            ICartService cartService,
            ICouponSerevice couponSerevice)
        {
            _productService = productService;
            _cartService = cartService;
            _couponSerevice = couponSerevice;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await FindUserCart());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(p => p.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.ApplyCoupon(model, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }    
        
        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoverCoupon()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(p => p.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(UserId, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }



        public async Task<IActionResult> Remove(int id)
        {

            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(p => p.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveFromCart(id, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Confirmar()
        {
            return View(await FindUserCart());
        }

        [HttpPost]
        public async Task<IActionResult> Confirmar(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.confirmar(model.CartHeader, token);

            if (response != null)
            {
                return RedirectToAction(nameof(Confirmation));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View(await FindUserCart());
        }

        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(p => p.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(UserId, token);

            if (response?.CartDetails != null)
            {
                if(!string.IsNullOrEmpty(response.CartHeader.CouponCode))
                {
                    var cupom = await _couponSerevice.GetCoupon(response.CartHeader.CouponCode,token);
                    if (cupom?.CodCupom != null) 
                    {
                        response.CartHeader.DescontoTotal = cupom.DescontoTotal;
                    }
                }
                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.ValorFinal += (detail.Product.Preco * detail.Count);

                }

                response.CartHeader.ValorFinal -= response.CartHeader.DescontoTotal;
            }
            return response;
        }

    }
}
