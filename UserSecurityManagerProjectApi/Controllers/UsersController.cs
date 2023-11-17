using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserSecurityManagerProjectApi.Models;
using UserSecurityManagerProjectApi.Persistence;
using UserSecurityManagerProjectApi.Services;

namespace UserSecurityManagerProjectApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        private readonly IEmailSender _emailSender;
        private readonly ILogger<UsersController> _logger;

        private readonly ApplicationDbContext _applicationDbContext;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            TokenService tokenService,
            ILogger<UsersController> logger,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;

            _applicationDbContext = applicationDbContext;

            _logger.LogInformation($"TEST");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Users", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);

                // Send email
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                return Ok(callbackUrl);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // Generate a secure token for setting password (this could be a JWT token or similar)
                var setPasswordToken = _tokenService.GenerateSetPasswordToken(user);

                // Create a URL to the set password page with the token as a query parameter
                var setPasswordUrl = $"https://yourfrontend.com/setpassword?useremail={user.Email}&token={setPasswordToken}";

                return Redirect(setPasswordUrl);
            }

            return BadRequest("Email confirmation failed");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("setpassword")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordModel model)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return BadRequest("User not found or email not confirmed");
            }

            var result = await _userManager.AddPasswordAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.HasPasswordAsync(user) && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = _tokenService.GenerateLoginToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [Authorize(Policy = "LoggedPolicy", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllDatabse()
        {
            return Ok(new { _applicationDbContext.Users, _applicationDbContext.Roles, _applicationDbContext.UserTokens, _applicationDbContext.UserLogins, _applicationDbContext.UserClaims });
        }
    }
}
