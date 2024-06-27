using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Product;


namespace TechStore.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody]ProductCreateModel product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid product model state.");
                return BadRequest(ModelState);
            }

            var existingProduct = await _productService.GetProductBySlugAsync(product.Slug);

            if (existingProduct != null)
            {
                _logger.LogInformation("A product with slug {Slug} already exists.", product.Slug);
                return BadRequest("A product with the specified slug already exists. Please choose a different slug.");
            }

            try
            {
                await _productService.CreateAsync(product);

                _logger.LogInformation("Product created successfully with slug {Slug}.", product.Slug);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            int deleteProductId = await _productService.DeleteAsync(id);
            
            return deleteProductId < 1 ? NotFound() : Ok(id);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody]ProductUpdateModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedProduct = await _productService.UpdateAsync(id, product);

                if (updatedProduct == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found.", id);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("Product {Code} successfully updated.", id);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id < 1)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(id);

            return (product is null) ? NotFound() : Ok(product);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductReadModel>> GetProductBySlug(string slug)
        {
            var product = await _productService.GetProductBySlugAsync(slug);

            return (product is null) ? NotFound() : Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("onsale")]
        public async Task<IActionResult> GetProductsOnSaleAsync()
        {
            var products = await _productService.GetProductsOnSaleAsync();

            return Ok(products);
        }

        [HttpGet("new")]
        public async Task<IActionResult> GetNewProducts()
        {
            var newProducts = await _productService.GetNewProductsAsync();

            return Ok(newProducts);
        }

        [HttpGet("bestsellers")]
        public async Task<IActionResult> GetTopSellingProducts()
        {
            var topSellingProducts = await _productService.GetTopSellingProductsAsync();

            return Ok(topSellingProducts);
        }


        [HttpGet("top")]
        public async Task<IActionResult> GetTopRatedProducts()
        {
            var topRatedProducts = await _productService.GetTopRatedProductsAsync();

            return Ok(topRatedProducts);
        }

        [HttpGet("subcategory/{id:int}")]
        public async Task<IActionResult> GetProductsBySubcategoryIdAsync(int id)
        {
            var products = await _productService.GetProductsBySubcategoryIdAsync(id);

            return (products is null) ? NotFound() : Ok(products);
        }

        [HttpGet("subcategory/{slug}")]
        public async Task<IActionResult> GetProductsBySubcategorySlug(string slug)
        {
            var products = await _productService.GetProductsBySubcategorySlugAsync(slug);

            return (products is null) ? NotFound() : Ok(products);
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> SearchProductsAsync(string search)
        {
            var products = await _productService.SearchProductsAsync(search);

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

        [HttpPost("specification")]
        public async Task<IActionResult> AddSpecification([FromBody] ProductSpecificationModel productSpecification)
        {
            if (productSpecification is null)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(productSpecification.ProductId);

            if (product == null)
                return NotFound();

            await _productService.AddSpecificationAsync(product.Id, productSpecification.ProductAttributes);
            return Ok();
        }
    }
}
