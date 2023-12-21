using LojaShopping.Web.Models;
using LojaShopping.Web.Models.Services.IService;
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

        public async Task<IActionResult> ProdutoIndex()
        {
            var produto = await _productService.FindAllProduct();
            return View(produto);
        }

        public IActionResult CriarProduto()
        {
            return View();
        }

        [HttpPost]
   
        public async Task<IActionResult> ProdutoCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProdutoIndex));
                }

            }
            return View(model);
        }

        public async Task<IActionResult> ProdutoUpdate(int id)
        {
            var model = await _productService.FindAllProductById(id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
   
        public async Task<IActionResult> ProdutoUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProdutoIndex));
                }

            }
            return View(model);
        }


        public async Task<IActionResult> ProdutoDelete(int id)
        {
            var model = await _productService.FindAllProductById(id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult> ProdutoDelete(ProductModel model)
        {
                var response = await _productService.DeleteProductById(model.Id);
                if (response)
                {
                    return RedirectToAction(nameof(ProdutoIndex));
                }
            return View(model);
        }
    }
}
