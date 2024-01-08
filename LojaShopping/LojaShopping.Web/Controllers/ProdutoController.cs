using LojaShopping.Web.Models;
using LojaShopping.Web.Models.Services.IService;
using LojaShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaShopping.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProductService _productService;

        public ProdutoController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        //[Authorize]
        public async Task<IActionResult> ProdutoIndex()
        {
            var products = await _productService.FindAllProducts("");
            return View(products);
        }

        public IActionResult CriarProduto()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CriarProduto(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(model, token);
                if (response != null) return RedirectToAction(
                     nameof(ProdutoIndex));
            }
            return View(model);
        }

        public async Task<IActionResult> ProdutoUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id, token);
            if (model != null) return View(model);
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProdutoUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct(model, token);
                if (response != null) return RedirectToAction(
                     nameof(ProdutoIndex));
            }
            return View(model);
        }

        //[Authorize]
        public async Task<IActionResult> ProdutoDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductById(id, token);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProdutoDelete(ProductModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id, token);
            if (response) return RedirectToAction(
                    nameof(ProdutoIndex));
            return View(model);
        }
    }
}
