using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.src.Models;
using GeekShopping.Web.src.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeekShopping.Web.src.Controllers
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
        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null) return View(product);

            return NotFound();
        }

        [HttpPost("ProductDelete")]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var response = await _productService.DeleteProduct(productModel.Id);
            if (response) return RedirectToAction(nameof(ProductIndex));
            return View(productModel);
        }
    }
}