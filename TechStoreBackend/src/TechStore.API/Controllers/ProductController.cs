using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Product;


namespace TechStore.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProductCreateModel product)
        {
            if (product == null)
                return BadRequest();
            
            await _productService.AddAsync(product);
            
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _productService.DeleteAsync(id);
            
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ProductUpdateModel product)
        {
            if (product == null)
                return BadRequest();

            await _productService.UpdateAsync(product);

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id < 1)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetProductBySlug(string slug)
        {
            var product = await _productService.GetProductBySlugAsync(slug);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("onsale")]
        public async Task<IActionResult> GetProductsOnSaleAsync()
        {
            var products = await _productService.GetProductsOnSaleAsync();

            return Ok(products);
        }

        [HttpGet("brand/{id:int}")]
        public async Task<IActionResult> GetProductsByBrandId(int id)
        {
            var products = await _productService.GetProductsByBrandIdAsync(id);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("brand/{name}")]
        public async Task<IActionResult> GetProductsByBrandNameAsync(string name)
        {
            var products = await _productService.GetProductsByBrandNameAsync(name);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("subcategory/{id:int}")]
        public async Task<IActionResult> GetProductsBySubcategoryIdAsync(int id)
        {
            var products = await _productService.GetProductsBySubcategoryIdAsync(id);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("subcategory/{name}")]
        public async Task<IActionResult> GetProductsBySubcategoryName(string name)
        {
            var products = await _productService.GetProductsBySubcategoryNameAsync(name);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var products = await _productService.GetProductsByNameAsync(name);

            return Ok(products);
        }

        [HttpGet("price")]
        public async Task<IActionResult> GetProductsByPrice(decimal priceFrom, decimal priceTo)
        {
            var products = await _productService.GetProductsByPriceAsync(priceFrom, priceTo);

            return Ok(products);
        }

        [HttpGet("rating")]
        public async Task<IActionResult> GetProductsByRating(decimal rating)
        {
            var products = await _productService.GetProductsByRatingAsync(rating);

            return Ok(products);
        }
    }
}
