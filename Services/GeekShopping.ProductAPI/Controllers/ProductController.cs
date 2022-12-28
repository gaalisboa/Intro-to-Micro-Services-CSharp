using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductVO>>> GetAll()
        {
            var products = await _repository.GetAll();
            if (!products.Any()) return NoContent();
            return Ok(products);
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> GetById(long id)
        {
            var product = await _repository.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO vo)
        {
            if (vo == null) return BadRequest();
            await _repository.Create(vo);
            return Created(nameof(Create), vo);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update(ProductVO vo)
        {
            if (vo == null) return BadRequest();
            await _repository.Update(vo);
            return Ok(true);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _repository.Delete(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }
    }
}