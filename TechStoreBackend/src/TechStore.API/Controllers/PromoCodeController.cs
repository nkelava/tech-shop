using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.PromoCode;


namespace TechStore.API.Controllers
{
    [Route("api/promo-codes")]
    [Authorize]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        public readonly IPromoCodeService _promoCodeService;
        public readonly IMapper _mapper;

        public PromoCodeController(IPromoCodeService promoCodeService, IMapper mapper)
        {
            _promoCodeService = promoCodeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromoCodeCreateModel promoCode)
        {
            if (promoCode is null)
                return BadRequest();

            try
            {
                await _promoCodeService.CreateAsync(promoCode);
                return Ok(promoCode);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PromoCodeUpdateModel promoCode)
        {
            if (promoCode is null)
                return BadRequest();

            var promoCodeToUpdate = await _promoCodeService.GetByCodeAsync(promoCode.Code);

            if (promoCodeToUpdate == null)
                return NotFound();

            try
            {
                await _promoCodeService.UpdateAsync(promoCode);
                return Ok(promoCode);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
               return BadRequest() ;

            int promoCodeId = await _promoCodeService.DeleteAsync(id);

            return promoCodeId > 0 ? Ok(promoCodeId) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promoCodes = await _promoCodeService.GetAllAsync();
            return Ok(promoCodes);
        }
    }
}
