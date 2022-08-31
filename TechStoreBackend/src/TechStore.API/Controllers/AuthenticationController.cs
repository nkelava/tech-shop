using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechStore.Application.Models.Authorization;
using TechStore.Domain.Entities.User;
using TechStore.Infrastructure.Data;


namespace TechStore.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        //private readonly JwtSettings _jwtSettings;
        private readonly TechStoreContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public readonly IMapper _mapper;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, IMapper mapper, TechStoreContext techStoreContext, TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            //_jwtSettings = jwtSettings;
            _configuration = configuration;
            _context = techStoreContext;
            _tokenValidationParameters = tokenValidationParameters;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);

                if (user != null)
                {
                    //User is found, try to sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                    if (result.Succeeded) {
                        var jwtToken = await GenerateJwtToken(user);
                        
                        return Ok(jwtToken);
                    }                        
                    return BadRequest("Wrong credentials. Please try again");
                }
                //User not found
                return NotFound("Wrong credentials. Please try again");
            }
            return BadRequest(loginModel);
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registerModel.Email);

                if (user != null)
                {
                    return BadRequest("This email address is already in use");
                }

                var newUser = new IdentityUser()
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email
                };
                var isCreatedResponse = await _userManager.CreateAsync(newUser, registerModel.Password);

                if (isCreatedResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                    //var jwtToken = await GenerateJwtToken(newUser);

                    //return Ok(jwtToken);
                    return Ok("Success");
                }
                return BadRequest("Server error");
            }
            return BadRequest(registerModel);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    return (result.Succeeded) ? Ok(result) : BadRequest(result);
                }

                return NotFound("User not found");
            }

            return BadRequest("Invalid properties");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequrest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);

                if (result == null)
                {
                    return BadRequest(new AuthResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid tokens"
                        },
                        Success = false
                    });
                }

                return Ok(result);
            }

            return BadRequest(new AuthResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid parameters"
                },
                Success = false
            });
        }

        private async Task<AuthResponse> VerifyAndGenerateToken(TokenRequrest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _tokenValidationParameters.ValidateLifetime = false;

                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)return null;
                }

                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.Now)
                {
                    return new AuthResponse()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Expired tokens"
                        }
                    };
                }

                var storedToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == tokenRequest.RefreshToken);
                var jti = tokenInVerification.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken == null || storedToken.isUsed || storedToken.isRevoked || storedToken.JwtId != jti)
                {
                    return new AuthResponse()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Invalid tokens"
                        }
                    };
                }

                if (storedToken.ExpiryDate < DateTime.UtcNow)
                {
                    return new AuthResponse()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Expired tokens"
                        }
                    };
                }

                storedToken.isUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(storedToken.UserId);

                return await GenerateJwtToken(user);
            }
            catch (Exception e)
            {
                return new AuthResponse()
                {
                    Success = false,
                    Errors = new List<string>()
                        {
                            "Internal error"
                        }
                };
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeValue = dateTimeValue.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeValue;
        }

        private async Task<AuthResponse> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetSection("JwtSettings:Audience").Value),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JwtSettings:Issuer").Value)
                }),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtSettings:ExpiryTimeFrame").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            // Refresh token section
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                Token = RandomStringGenerator(23), // Generate a refresh token
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                isRevoked = false,
                isUsed = false,
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshToken); 
            await _context.SaveChangesAsync();

            return new AuthResponse()
            {
                Success = true,
                RefreshToken = refreshToken.Token,
                Token = jwtToken
            };
        }

        private string RandomStringGenerator(int length)
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
