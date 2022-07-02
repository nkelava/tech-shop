using Microsoft.AspNetCore.Mvc;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //public readonly IProductService _productService;

        //public ProductController(IProductService productService)
        //{

        //}


        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = new List<Product>
            {
                new Product {Name = " Product 1"},
                new Product {Name = "Product 2"}
            };

            return products;
        }
    }
}
