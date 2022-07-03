using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Brand;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService _brandService;
        public readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();

            return brands;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            if (id < 1)
                return BadRequest();

            var brand = await _brandService.GetBrandByIdAsync(id);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]BrandCreateModel brand)
        {
            if (brand == null)
                return BadRequest();

            await _brandService.CreateBrand(brand);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]BrandUpdateModel brand)
        {
            if (brand == null)
                return BadRequest();

            await _brandService.UpdateBrand(brand);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _brandService.DeleteBrand(id);

            return Ok();
        }
    }
}
