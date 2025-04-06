using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfileController : ControllerBase
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileService _profileService;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = "user")]
		[HttpGet]
        public int GetProfile()
        {
            return 1;
        }

		[Authorize(Policy = "user")]
		[HttpPut]
		public int PutProfile()
		{
			return 1;
		}

		[Authorize(Policy = "user")]
		[HttpGet("achievements")]
		public async Task<ActionResult<ICollection<Achievement>>> GetAchievemnts
            (CancellationToken cancellationToken)
		{
            Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirst("UserId")!.ToString(), out userId);

            if (!flag)
            {
                return BadRequest();
            }
            var result = await _profileService.GetAchievements(userId, cancellationToken);

            if (result.IsError)
            {
                return BadRequest();
            }

			return Ok(result.Value);
		}

		[Authorize(Policy = "user")]
		[HttpDelete]
        public int DeleteProfile()
        {
            return -1;
        }


	}
}
