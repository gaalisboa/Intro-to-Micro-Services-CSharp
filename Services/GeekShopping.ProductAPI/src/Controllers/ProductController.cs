using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.src.Data.ValueObjects;
using GeekShopping.ProductAPI.src.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeekShopping.ProductAPI.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _repository;

        public ProductController(ILogger<ProductController> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductVO>>> GetAll()
        {
            var products = await _repository.GetAll();
            if (!products.Any()) return NoContent();
            return Ok(products);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductVO>> GetById(long id)
        {
            var product = await _repository.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ProductVO>> Create(ProductVO vo)
        {
            if (vo == null) return BadRequest();
            await _repository.Create(vo);
            return Created(nameof(Create), vo);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductVO vo)
        {
            if (vo == null) return BadRequest();
            await _repository.Update(vo);
            return Ok(true);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _repository.Delete(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }
    }
}