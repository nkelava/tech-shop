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

            var existingProduct = await _productService.GetBySlugAsync(product.Slug);

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

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateModel product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid product update request for product ID {id}. ModelState: {@ModelState}", id, ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                var updatedProduct = await _productService.UpdateAsync(id, product);

                if (updatedProduct == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found.", id);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("Product {id} successfully updated.", id);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid product ID {id} for delete request.", id);
                return BadRequest("Invalid product ID.");
            }

            try
            {
                int? deleteProductId = await _productService.DeleteAsync(id);

                if (deleteProductId == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found for delete request.", id);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("Product {id} successfully deleted.", id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id < 1)
            {
                _logger.LogWarning("Invalid product ID {id} for GetById request.", id);
                return BadRequest("Invalid product ID.");
            }
            
            try
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID {id} was not found for GetById request.", id);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("Product {id} retrieved successfully.", id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductReadModel>> GetBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                _logger.LogWarning("Invalid product slug '{slug}' for GetBySlug request.", slug);
                return BadRequest("Invalid product slug.");
            }
            
            try
            {
                var product = await _productService.GetBySlugAsync(slug);

                if (product == null)
                {
                    _logger.LogWarning("Product with slug '{slug}' was not found for GetBySlug request.", slug);
                    return NotFound("Product not found.");
                }

                _logger.LogInformation("Product with slug '{slug}' retrieved successfully.", slug);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the product by slug '{slug}'.", slug);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("hot-offers")]
        public async Task<IActionResult> GetProductsOnSaleAsync()
        {
            try
            {
                var hotOffers = await _productService.GetHotOffersAsync();

                _logger.LogInformation("Retrieved hot offer products successfully.");
                return Ok(hotOffers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving hot products.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("new")]
        public async Task<IActionResult> GetNewProducts()
        {
            try
            {
                var newProducts = await _productService.GetNewProductsAsync();

                _logger.LogInformation("Retrieved new products successfully.");
                return Ok(newProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving new products.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("bestsellers")]
        public async Task<IActionResult> GetTopSellingProducts()
        {
            try
            {
                var bestSellers = await _productService.GetTopSellingProductsAsync();

                _logger.LogInformation("Retrieved top selling products successfully.");
                return Ok(bestSellers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving top selling products.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpGet("top")]
        public async Task<IActionResult> GetTopRatedProducts()
        {
            try
            {
                var topRated = await _productService.GetTopRatedProductsAsync();

                _logger.LogInformation("Retrieved top rated products successfully.");
                return Ok(topRated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving top rated products.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("subcategory/{id:int}")]
        public async Task<IActionResult> GetProductsBySubcategoryIdAsync(int id)
        {
            try
            {
                var products = await _productService.GetProductsBySubcategoryIdAsync(id);

                if (products == null)
                {
                    _logger.LogWarning("Subcategory with ID {Id} not found.", id);
                    return NotFound("Subcategory not found.");
                }

                _logger.LogInformation("Retrieved products for subcategory ID {id} successfully.", id);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products for subcategory ID {id}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("subcategory/{slug}")]
        public async Task<IActionResult> GetProductsBySubcategorySlug(string slug)
        {
            try
            {
                var products = await _productService.GetProductsBySubcategorySlugAsync(slug);

                if (products == null)
                {
                    _logger.LogWarning("Subcategory with slug {Slug} not found.", slug);
                    return NotFound("Subcategory not found.");
                }

                _logger.LogInformation("Retrieved products for subcategory with slug '{slug}' successfully.", slug);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products for subcategory with slug '{slug}'.", slug);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> SearchProductsAsync(string search)
        {
            try
            {
                var products = await _productService.SearchProductsAsync(search);

                _logger.LogInformation("Retrieved products for search query '{search}' successfully.", search);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching products for query '{search}'.", search);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("price")]
        public async Task<IActionResult> GetProductsByPrice(decimal priceFrom, decimal priceTo)
        {
            try
            {
                var products = await _productService.GetProductsByPriceAsync(priceFrom, priceTo);

                _logger.LogInformation("Retrieved products within the price range {priceFrom} - {priceTo} successfully.", priceFrom, priceTo);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products within the price range {priceFrom} - {priceTo}.", priceFrom, priceTo);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("rating")]
        public async Task<IActionResult> GetProductsByRating(decimal rating)
        {
            try
            {
                var products = await _productService.GetProductsByRatingAsync(rating);

                _logger.LogInformation("Retrieved products with rating {rating} successfully.", rating);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products with rating {rating}.", rating);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();

                _logger.LogInformation("Retrieved products successfully.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("specification/{id:int}")]
        public async Task<IActionResult> AddSpecification(int id, [FromBody] ProductSpecificationModel productSpecification)
        {
            if (id < 0)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var product = await _productService.AddSpecificationAsync(id, productSpecification.ProductAttributes);

            if (product == null)
                return NotFound();

            return Ok();
        }
    }
}
