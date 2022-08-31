using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace TechStore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //public readonly IUserService _userService;
        public readonly IMapper _mapper;

        //public UserController(IUserService userService, IMapper mapper)
        public UserController(IMapper mapper)
        {
            //_userService = userService;
            _mapper = mapper;
        }
    }
}
