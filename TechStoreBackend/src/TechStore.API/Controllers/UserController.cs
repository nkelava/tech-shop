using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechStore.Application.Models.User;
using TechStore.Domain.Entities.User;


namespace TechStore.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(currentUserEmail);
            
            return user is null ? NotFound() : Ok(new {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            return (user is null) ? NotFound() : Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var newUsers = new List<UserReadModel>();

            foreach (var user in users)
            {
                newUsers.Add(new UserReadModel
                {
                    Info = user,
                    IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                });
            };

            return Ok(newUsers);
        }

        [HttpPost("edit/profile")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserUpdateProfileModel profileModel)
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            if (user is null)
                return NotFound();

            user.FirstName = profileModel.FirstName;
            user.LastName = profileModel.LastName;
            user.PhoneNumber = profileModel.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPost("edit/email")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UserUpdateEmailModel emailModel)
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            if (user is null)
                return NotFound();

            //await _userManager.SetEmailAsync(user, emailModel.NewEmail);
            user.Email = emailModel.NewEmail;
            await _userManager.UpdateAsync(user);

            return Ok();
        }


        [HttpPost("edit/password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserUpdatePasswordModel passwordModel)
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            if (user is null)
                return NotFound();

            await _userManager.ChangePasswordAsync(user, passwordModel.CurrentPassword, passwordModel.NewPassword);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            string currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(currentUserEmail))
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            if (user is null)
                return NotFound();

            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return NotFound();

            await _userManager.DeleteAsync(user);
            return Ok();
        }
    }
}
