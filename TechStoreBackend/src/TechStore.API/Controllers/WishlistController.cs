using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace TechStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        public readonly IMapper _mapper;

        public WishlistController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
