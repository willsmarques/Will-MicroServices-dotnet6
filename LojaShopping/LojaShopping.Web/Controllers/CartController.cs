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

        public CartController(IProductService productService,
            ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }


        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await FindUserCart());
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

        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(p => p.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(UserId, token);

            if (response?.CartDetails != null)
            {
                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.ValorFinal += (detail.Product.Preco * detail.Count);

                }
            }
            return response;
        }

    }
}
