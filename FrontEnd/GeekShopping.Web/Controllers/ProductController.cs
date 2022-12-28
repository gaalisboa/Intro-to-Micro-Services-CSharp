using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet("ProductCreate")]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost("ProductCreate")]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(productModel);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(productModel);
        }

        [HttpGet("ProductUpdate")]
        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null) return View(product);

            return NotFound();
        }

        [HttpPost("ProductUpdate")]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(productModel);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(productModel);
        }

        [HttpGet("ProductDelete")]
        [Authorize]
        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null) return View(product);

            return NotFound();
        }

        [HttpPost("ProductDelete")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var response = await _productService.DeleteProduct(productModel.Id);
            if (response) return RedirectToAction(nameof(ProductIndex));
            return View(productModel);
        }
    }
}