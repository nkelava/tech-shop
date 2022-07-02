using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IEnumerable<Brand>> Get()
        {
            var brands = await _brandService.GetAllBrands();

            return brands;
        }

        //[HttpGet]
        //public ActionResult<List<Brand>> GetAllBrands()
        //{
        //    var brands = new List<Brand>
        //    {
        //        new Brand {Name = "Acer"},
        //        new Brand {Name = "Asus"}
        //    };

        //    return brands;
        //}

    }
}
