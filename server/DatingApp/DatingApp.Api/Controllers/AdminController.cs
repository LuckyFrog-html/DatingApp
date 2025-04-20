using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Application.Services;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using DatingApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly ILogger<AuthController> _logger;
		private readonly IHobbyRepository _hobbyRepository;
		private readonly IProfileService _profileService;

		public AdminController(ILogger<AuthController> logger,
			IHobbyRepository hobbyRepository,
			IProfileService profileService)
		{
			_logger = logger;
			_hobbyRepository = hobbyRepository;
			_profileService = profileService;
		}

		[Authorize(Policy = "admin")]
		[HttpPost("hobbies")]
		public async Task<ActionResult> AddHobbies
			(HobbyRequest addedHobby, CancellationToken cancellationToken)
		{
			var newHobby = new Hobby
			{
				Id = Guid.NewGuid(),
				Name = addedHobby.Name,
				Description = addedHobby.Description,
			};

			var result = await _hobbyRepository.AddAsync(newHobby, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}


		[Authorize(Policy = "admin")]
		[HttpPatch("editProfile")]
		public async Task<ActionResult> EditProfile
			(Guid userId,
			ProfilePatchRequest updatedProfileInfo,
			CancellationToken cancellationToken)
		{
			
			var result = await _profileService.EditProfile(userId, updatedProfileInfo, cancellationToken);
			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok();
		}


		[Authorize(Policy = "admin")]
		[HttpDelete]
		public async Task<ActionResult> MarkAsDeleteProfile(
			Guid userId,
			CancellationToken cancellationToken)
		{
			var result = await _profileService.MarkAsDeletedAsync(userId, cancellationToken);

			if (result.IsError)
			{
				return BadRequest();
			}

			return Ok(result.Value);
		}
	}
}
