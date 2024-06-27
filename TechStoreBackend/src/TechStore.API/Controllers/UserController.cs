using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp.Authenticators;
using RestSharp;
using System.Net;
using System.Security.Claims;
using TechStore.Application.Models.User;
using TechStore.Domain.Entities.User;


namespace TechStore.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(currentUserEmail);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", currentUserEmail);
                    return NotFound("User not found.");
                }

                var userModel = new
                {
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.Email
                };

                _logger.LogInformation("User with email {Email} fetched successfully.", currentUserEmail);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user information.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userReadModels = new List<UserReadModel>();

                foreach (var user in users)
                {
                    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    userReadModels.Add(new UserReadModel
                    {
                        Info = user,
                        IsAdmin = isAdmin
                    });
                }

                _logger.LogInformation("Fetched all users successfully.");
                return Ok(userReadModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all users.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpPost("edit/profile")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserUpdateProfileModel profileModel)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid profile model received: {ModelStateErrors}", ModelState);
                return BadRequest("Invalid profile data. Please check the details and try again.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(currentUserEmail);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", currentUserEmail);
                    return NotFound("User not found.");
                }

                user.FirstName = profileModel.FirstName;
                user.LastName = profileModel.LastName;
                user.PhoneNumber = profileModel.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to update user with email {Email}. Errors: {Errors}", currentUserEmail, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to update user profile.");
                }

                _logger.LogInformation("User with email {Email} updated their profile successfully.", currentUserEmail);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user profile for email {Email}.", currentUserEmail);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpPost("edit/email")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UserUpdateEmailModel emailModel)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid email model received: {ModelStateErrors}", ModelState);
                return BadRequest("Invalid email data. Please check the details and try again.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(currentUserEmail);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", currentUserEmail);
                    return NotFound("User not found.");
                }

                var emailResult = await _userManager.SetEmailAsync(user, emailModel.NewEmail);
                var usernameResult = await _userManager.SetUserNameAsync(user, emailModel.NewEmail);

                if (!emailResult.Succeeded || !usernameResult.Succeeded)
                {
                    _logger.LogWarning("Failed to update email for user with email {Email}. Errors: {Errors}", currentUserEmail, string.Join(", ", emailResult.Errors.Select(e => e.Description)));
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to update email.");
                }

                var verificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Authentication",
                    new { userId = user.Id, token = verificationToken },
                    protocol: Request.Scheme);

                var emailBody =
                    $"<h1 style=\"margin-bottom: 10px;\">Email verification</h1>" +
                    $"<p style=\"margin-bottom: 40px;\">Wowwee! We're excited to have you get started. Before we get started, we will need to confirm your account. You can do that by simply clickling on the button below.</p>" +
                    $"<a href=\"{callbackUrl}\" style=\"background-color: #49656a; color: #ffffff; border: none; outline: none; border-radius: 5px; padding: 10px 20px; text-decoration: none;\"> Confirm Account </a>" +
                    $"<p style=\"margin-top: 40px; margin-bottom: 20px;\">Or, if that doesn't work, copy and paste the following link in your browser:</p> " +
                    $"<p style=\"margin-bottom: 20px;\">{callbackUrl}</p>" +
                    $"<p>If you didn't create an account with TechPlanet, you can safely delete this email.<br/><br/>Cheers, <br/> TechPlanet</p>";

                var emailSent = SendEmail(emailBody, user.Email);

                if (emailSent)
                {
                    _logger.LogInformation("User with email {Email} changed their password successfully.", currentUserEmail);
                    return Ok("Please, verify your email. Confirmation link is sent to your email address.");
                }

                return StatusCode((int)HttpStatusCode.Forbidden, "Please, request an email verification link.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the email for user {Email}.", currentUserEmail);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }


        [HttpPost("edit/password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserUpdatePasswordModel passwordModel)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid password model received: {ModelStateErrors}", ModelState);
                return BadRequest("Invalid password data. Please check the details and try again.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(currentUserEmail);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", currentUserEmail);
                    return NotFound("User not found.");
                }

                var result = await _userManager.ChangePasswordAsync(user, passwordModel.CurrentPassword, passwordModel.NewPassword);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to change password for user with email {Email}. Errors: {Errors}", currentUserEmail, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Failed to change password." });
                }

                _logger.LogInformation("User with email {Email} changed their password successfully.", currentUserEmail);
                return Ok(new { Message = "Password updated successfully." });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing the password for user {Email}.", currentUserEmail);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(currentUserEmail))
            {
                _logger.LogWarning("Unauthorized access attempt.");
                return Unauthorized("User is not authorized.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(currentUserEmail);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", currentUserEmail);
                    return NotFound("User not found.");
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to delete user with email {Email}. Errors: {Errors}", currentUserEmail, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to delete user.");
                }

                _logger.LogInformation("User with email {Email} was deleted successfully.", currentUserEmail);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user with email {Email}.", currentUserEmail);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("Bad request: email is null or empty.");
                return BadRequest("Email cannot be null or empty.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", email);
                    return NotFound("User not found.");
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to delete user with email {Email}. Errors: {Errors}", email, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to delete user.");
                }

                _logger.LogInformation("User with email {Email} was deleted successfully.", email);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user with email {Email}.", email);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("promote/{email}")]
        public async Task<IActionResult> Promote(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("Bad request: email is null or empty.");
                return BadRequest("Email cannot be null or empty.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", email);
                    return NotFound("User not found.");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var currentRole = roles.FirstOrDefault();

                if (currentRole == UserRoles.Admin)
                {
                    _logger.LogInformation("User with email {Email} is already an admin.", email);
                    return Ok("User is already an admin.");
                }

                // Remove current role and add Admin role
                if (!string.IsNullOrWhiteSpace(currentRole))
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                }

                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

                _logger.LogInformation("User with email {Email} promoted to admin successfully.", email);
                return Ok("User promoted to admin successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while promoting user with email {Email}.", email);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later." );
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("demote/{email}")]
        public async Task<IActionResult> Demote(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("Bad request: email is null or empty.");
                return BadRequest("Email cannot be null or empty.");
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} was not found.", email);
                    return NotFound("User not found.");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var isAdmin = roles.Contains(UserRoles.Admin);

                if (!isAdmin)
                {
                    _logger.LogInformation("User with email {Email} is not an admin, no action taken.", email);
                    return Ok("User is not an admin, no action taken.");
                }

                // Demote user by removing Admin role and adding User role
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
                await _userManager.AddToRoleAsync(user, UserRoles.User);

                _logger.LogInformation("User with email {Email} demoted from admin successfully.", email);
                return Ok("User demoted from admin successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while demoting user with email {Email}.", email);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request. Please try again later.");
            }
        }

        private Boolean SendEmail(string body, string email)
        {
            var options = new RestClientOptions()
            {
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
