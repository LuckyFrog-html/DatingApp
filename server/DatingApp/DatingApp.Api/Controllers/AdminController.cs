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

		public AdminController(ILogger<AuthController> logger, IHobbyRepository hobbyRepository)
		{
			_logger = logger;
			_hobbyRepository = hobbyRepository;
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
	}
}
