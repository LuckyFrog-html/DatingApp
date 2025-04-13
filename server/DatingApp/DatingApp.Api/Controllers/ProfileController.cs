using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading;

namespace DatingApp.Api.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ProfileController : ControllerBase
	{

		private readonly ILogger<ProfileController> _logger;
		private readonly IProfileService _profileService;

		public ProfileController(ILogger<ProfileController> logger,
			IProfileService profileService)
		{
			_logger = logger;
			_profileService = profileService;
		}

		[Authorize(Policy = "user")]
		[HttpGet]
		public async Task<ActionResult<Profile>> GetProfile(CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirst("UserId")!.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			var result = await _profileService.GetProfileByIdAsync(userId, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
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
				return Unauthorized();
			}
			var result = await _profileService.GetAchievements(userId, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}

		[Authorize(Policy = "user")]
		[HttpGet("hobbies")]
		public async Task<ActionResult<ICollection<Hobby>>> GetHobbies
			(CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirst("UserId")!.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			var result = await _profileService.GetHobbiesAsync(userId, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}

		[Authorize(Policy = "user")]
		[HttpPost("hobbies")]
		public async Task<ActionResult> AddHobbies
			(ICollection<string> addedHobbies, CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirst("UserId")!.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			var result = await _profileService.AddHobbyAsync(userId, addedHobbies, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}




		[Authorize(Policy = "user")]
		[HttpDelete]
        public async Task<ActionResult> DeleteProfile(CancellationToken cancellationToken)
        {
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirst("UserId")!.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			var result = await _profileService.MarkAsDeletedAsync(userId, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}


	}
}
