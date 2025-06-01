using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Application.Services;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace DatingApp.Api.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ProfileController : ControllerBase
	{

		private readonly ILogger<ProfileController> _logger;
		private readonly IProfileService _profileService;
		private readonly IUserService _userService;
		private readonly IHobbyRepository _hobbyRepository;

		public ProfileController(ILogger<ProfileController> logger,
			IProfileService profileService,
			IUserService userService,
			IHobbyRepository hobbyRepository)
		{
			_logger = logger;
			_profileService = profileService;
			_userService = userService;
			_hobbyRepository = hobbyRepository;
		}

		[Authorize(Policy = "user")]
		[HttpGet]
		public async Task<ActionResult<object>> GetProfile(CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")?.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			var result = await _profileService.GetProfileByIdAsync(userId, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			var profile = result.Value;
			var balanceString = profile.Balance.ToString();
			return Ok(
				new {
					profile.Id,
					profile.Name,
					profile.Description,
					profile.Age,
					profile.Town,
					profile.Gender,
					balanceString,
					profile.IsDeleted,
					profile.Hobbies,
					profile.Achievements
				}
			);
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
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")!.ToString(), out userId);

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
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")!.ToString(), out userId);

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
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")!.ToString(), out userId);

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

		[HttpGet("allhobbies")]
		public async Task<ActionResult<ICollection<Hobby>>> GetAllHobbies(CancellationToken cancellationToken)
		{
			var result = await _hobbyRepository.GetAllAsync(cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}

		//[Authorize(Policy = "user")]
		[HttpPost("createprofile")]
		public async Task<ActionResult> CreateProfile(string email, RegisterProfileRequest profileReq,
			CancellationToken cancellationToken) 
		{
			//Guid userId;
			//var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId").ToString(), out userId);

			//if (!flag)
			//{
			//	return Unauthorized();
			//}
			var test = (await _userService.GetUserByEmailAsync(email, cancellationToken)).Value;
			Guid userId = test.Id;
			var result = await _profileService.CreateProfile(
				userId,
				profileReq.Name,
				profileReq.Age,
				profileReq.Town,
				profileReq.Gender,
				cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}

		[Authorize(Policy = "user")]
		[HttpPatch("editProfile")]
		public async Task<ActionResult> EditProfile
			(ProfilePatchRequest updatedProfileInfo, CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")!.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}

			var result = await _profileService.EditProfile(userId, updatedProfileInfo, cancellationToken);
			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok();
		}




		[Authorize(Policy = "user")]
		[HttpDelete]
        public async Task<ActionResult> DeleteProfile(CancellationToken cancellationToken)
        {
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")!.ToString(), out userId);

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
