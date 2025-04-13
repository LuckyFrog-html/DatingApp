using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Application.Models.Responses;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(ILogger<AuthController> logger, 
            IUserService userService, 
            IAuthService authService)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest, 
            CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(registerRequest, cancellationToken);
            if (result.IsError)
            {
				var error = result.Errors.First();
				return error.Type switch
				{
					ErrorType.Validation => BadRequest(error.Description),
					ErrorType.NotFound => NotFound(error.Description),
					_ => StatusCode(500, "Internal server error")
				};
			}

            return Ok();

        }

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login(
            LoginRequest loginRequest,
            CancellationToken cancellationToken)
		{
			var result = await _authService.LoginAsync(loginRequest.Username,
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
    }
}
