using DatingApp.Application.Core.Interfaces;
using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Application.Models.Responses;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace DatingApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
		private readonly IMemoryCache _cache;
		private readonly IEmailService _emailService;

		public AuthController(ILogger<AuthController> logger, 
            IUserService userService, 
            IAuthService authService,
            IMemoryCache memoryCache,
			IEmailService emailService)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;
            _cache = memoryCache;
			_emailService = emailService;
        }

		

		[HttpPost("register")]
		public async Task<ActionResult> Register(RegisterRequest registerRequest,
			CancellationToken cancellationToken)
		{
			var errorOrResult = await _userService.IsUserExists(registerRequest.Email, cancellationToken);
			if (errorOrResult.IsError)
			{
				return BadRequest("");
			}

			var result = errorOrResult.Value;
			if (result == true)
			{
				return Conflict("User with this email already exists");
			}

			var code = new Random().Next(100000, 999999).ToString();

			_cache.Set(registerRequest.Email,
				(Password: registerRequest.Password, Code: code),
				TimeSpan.FromMinutes(20));

			await _emailService.SendEmailAsync(registerRequest.Email, "Код подтверждения",
				$"Ваш код подтверждения: {code}");

			return Ok();

		}

		[HttpPost("verifycode")]
        public async Task<ActionResult> VerifyCode(string email, string code, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue(email, out (string Password, string Code) cachedData))
            {
                return BadRequest("Код истек или не существует");
            }
            if (cachedData.Code != code)
            {
                return BadRequest("Неверный код");
            }

			return await CreateUser(email, cachedData.Password, cancellationToken);
        }

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login(
            LoginRequest loginRequest,
            CancellationToken cancellationToken)
		{
			var result = await _authService.LoginAsync(loginRequest.Email,
                loginRequest.Password,
                cancellationToken);


            if (result.IsError)
            {
				var error = result.Errors.First();
				return error.Type switch
				{
					ErrorType.Validation => BadRequest(error.Description),
					ErrorType.Unauthorized => Unauthorized(error.Description),
					ErrorType.NotFound => NotFound(error.Description),
					ErrorType.Conflict => Conflict(error.Description),
					_ => StatusCode(500, "Internal server error")
				};
			}
            LoginResponse loginResponse = result.Value;
            SetJwtCookie(HttpContext, loginResponse.AccessToken, loginResponse.RefreshToken);

            return Ok(loginResponse);
		}

        private void SetJwtCookie(
            HttpContext httpContext,
            string accessToken,
            string refreshToken)
        {
			httpContext.Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions
			{
				HttpOnly = false,
				Secure = true,
				SameSite = SameSiteMode.Lax,
				Expires = DateTime.UtcNow.AddMinutes(60)
			});

			httpContext.Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Lax
			});
		}

		private async Task<ActionResult> CreateUser(string email,
			string password,
			CancellationToken cancellationToken)
		{
			var result = await _userService.CreateUserAsync(email,
				password,
				cancellationToken);
			if (result.IsError)
			{
				var error = result.Errors.First();
				return error.Type switch
				{
					ErrorType.Conflict => Conflict(error.Description),
					ErrorType.Validation => BadRequest(error.Description),
					ErrorType.NotFound => NotFound(error.Description),
					_ => StatusCode(500, "Internal server error")
				};
			}

			return Ok();

		}
	}
}
