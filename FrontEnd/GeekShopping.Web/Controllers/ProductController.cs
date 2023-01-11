using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
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
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetAllProducts("");
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
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(productModel, token);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(productModel);
        }

        [HttpGet("ProductUpdate")]
        public async Task<IActionResult> ProductUpdate(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.GetProductById(id, token);
            if (product != null) return View(product);

            return NotFound();
        }

        [HttpPost("ProductUpdate")]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct(productModel, token);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(productModel);
        }

        [HttpGet("ProductDelete")]
        [Authorize]
        public async Task<IActionResult> ProductDelete(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.GetProductById(id, token);
            if (product != null) return View(product);

            return NotFound();
        }

        [HttpPost("ProductDelete")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProduct(productModel.Id, token);
            if (response) return RedirectToAction(nameof(ProductIndex));
            return View(productModel);
        }
    }
}