using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Brand;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.API.Controllers
{
    [Route("api/brand")]
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


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]BrandCreateModel brand)
        {
            if (brand == null)
                return BadRequest();

            await _brandService.AddAsync(brand);

            return Ok(brand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _brandService.DeleteAsync(id);

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]BrandUpdateModel brand)
        {
            if (brand == null)
                return BadRequest();

            await _brandService.UpdateAsync(brand);

            return Ok(brand);
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

        [HttpGet("/api/brands")]
        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();

            return brands;
        }
    }
}
