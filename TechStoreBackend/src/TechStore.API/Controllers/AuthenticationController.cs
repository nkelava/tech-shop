using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
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
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly SignInManager<ApplicationUser> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        //private readonly JwtSettings _jwtSettings;
        private readonly TechStoreContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public readonly IMapper _mapper;

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, 
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);

                if (user is not null) {
                    if (!user.EmailConfirmed)
                        return BadRequest("Please confirm your email address via link that is sent to you email address.");

                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                    if (result.Succeeded) {
                        var jwtToken = await GenerateJwtToken(user);
                        return Ok(jwtToken);
                    }                        
                    return BadRequest("Wrong credentials. Please try again.");
                }
                return NotFound("Wrong credentials. Please try again.");
            }
            return BadRequest(loginModel);
        }

        [AllowAnonymous]
        [HttpGet("register-admin")]
        public async Task<IActionResult> RegisterAdmin()
        {
            try
            {

                var newUser = new ApplicationUser()
                {
                    Email = "admin2@gmail.com",
                    UserName = "admin2@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true
                };

                var user = await _userManager.FindByEmailAsync("admin2@gmail.com");

                if (user is not null)
                    return BadRequest("The email address is already in use.");

                var isCreatedResponse = await _userManager.CreateAsync(newUser, "Admin_994");

                if (isCreatedResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                }

                return Ok(newUser);
            } catch
            {
                return BadRequest("Oh no");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(registerModel.Email);

                if (user is not null)
                    return BadRequest("The email address is already in use.");

                var newUser = new ApplicationUser()
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    EmailConfirmed = false
                };

                var isCreatedResponse = await _userManager.CreateAsync(newUser, registerModel.Password);

                if (isCreatedResponse.Succeeded) {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                    var verificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Authentication", new { userId = newUser.Id, token = verificationToken });

                    var emailBody =
                        $"<h1 style=\"margin-bottom: 10px;\">Email verification</h1>" +
                        $"<p style=\"margin-bottom: 40px;\">Wowwee! We're excited to have you get started. Before we get started, we will need to confirm your account. You can do that by simply clickling on the button below.</p>" +
                        $"<a href=\"{callbackUrl}\" style=\"background-color: #49656a; color: #ffffff; border: none; outline: none; border-radius: 5px; padding: 10px 20px; text-decoration: none;\"> Confirm Account </a>" +
                        $"<p style=\"margin-top: 40px; margin-bottom: 20px;\">Or, if that doesn't work, copy and paste the following link in your browser:</p> " +
                        $"<p style=\"margin-bottom: 20px;\">{callbackUrl}</p>" +
                        $"<p>If you didn't create an account with TechPlanet, you can safely delete this email.<br/><br/>Cheers, <br/> TechPlanet</p>";

                    var result = SendEmail(emailBody, newUser.Email);

                    if (result)
                        return Ok("Please, verify your email. Confirmation link is sent to your email address.");

                    return Ok("Please, request an email verification link.");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Register was unsuccessful. Please, try again later.");
            }
            return BadRequest(registerModel);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId is null || token is null)
                return BadRequest("Invalid email confirmation link.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return BadRequest("Invalid email parameters.");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "Your email is not confirmed.Please, try again later.");

            return Redirect(_configuration.GetSection("Client:AuthURL").Value);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully.");
        }

        [AllowAnonymous]
        [HttpPost("role")]
        public async Task<IActionResult> GetRole(TokenRequrest tokenRequest) {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokenRequest.Token);
            var userEmail = jwtSecurityToken.Payload["email"].ToString();
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(userRoles);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null) {
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    return (result.Succeeded) ? Ok(result) : BadRequest(result);
                }
                return NotFound("Not found.");
            }
            return BadRequest("Bad request.");
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequrest tokenRequest)
        {
            if (ModelState.IsValid) {
                var jwtToken = await VerifyAndGenerateToken(tokenRequest);

                if (jwtToken == null) {
                    return BadRequest(new AuthResponse()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Invalid tokens"
                        },
                    });
                }
                return Ok(jwtToken);
            }
            return BadRequest(new AuthResponse()
            {
                Success = false,
                Errors = new List<string>() {
                    "Invalid parameters"
                }
            });
        }

        private async Task<AuthResponse> VerifyAndGenerateToken(TokenRequrest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try {
                _tokenValidationParameters.ValidateLifetime = false;

                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken) {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false) return null;
                }

                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.Now) {
                    return new AuthResponse()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Expired tokens"
                        }
                    };
                }

                var storedToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == tokenRequest.RefreshToken);
                var jti = tokenInVerification.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                if (storedToken == null || storedToken.IsUsed || storedToken.IsRevoked || storedToken.JwtId != jti) {
                    return new AuthResponse() {
                        Success = false,
                        Errors = new List<string>() {
                            "Invalid tokens"
                        }
                    };
                }

                if (storedToken.ExpiryDate < DateTime.UtcNow) {
                    return new AuthResponse()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Expired tokens"
                        }
                    };
                }

                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(storedToken.UserId);

                return await GenerateJwtToken(user);
            }
            catch (Exception e) {
                return new AuthResponse() {
                    Success = false,
                    Errors = new List<string>() {
                        "Internal error"
                    }
                };
            }
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeValue = dateTimeValue.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeValue;
        }

        private async Task<AuthResponse> GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:SecretKey").Value);
            var userRoles = await _userManager.GetRolesAsync(user);

            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new[] {
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

            foreach (var role in userRoles)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                tokenDescriptor.Subject.AddClaim(claim);
            }

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken() {
                JwtId = token.Id,
                Token = RandomStringGenerator(23), // Generate a refresh token
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshToken); 
            await _context.SaveChangesAsync();

            return new AuthResponse() {
                Success = true,
                RefreshToken = refreshToken.Token,
                Token = jwtToken
            };
        }

        private static string RandomStringGenerator(int length)
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private Boolean SendEmail(string body, string email)
        {
            var options = new RestClientOptions() {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api", _configuration.GetSection("EmailConfig:API_KEY").Value)
            };
            var client = new RestClient(options);
            var request = new RestRequest();

            request.AddParameter("domain", "sandbox582822b6660543f09628c673c33be7b1.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <mailgun@sandbox582822b6660543f09628c673c33be7b1.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Tech Planet - Email Verification");
            request.AddParameter("html", body);
            request.Method = Method.Post;

            var response = client.Execute(request);

            return response.IsSuccessful;
        }
    }
}
