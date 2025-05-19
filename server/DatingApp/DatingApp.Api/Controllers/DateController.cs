using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DateController : ControllerBase
    {

        private readonly ILogger<DateController> _logger;
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
		private readonly IMemoryCache _cache;
		private readonly IAchievementIssuingService _achievementIssuingService;


		public DateController(ILogger<DateController> logger,
            IUserService userService,
            IProfileService profileService,
			IAchievementIssuingService achievementIssuingService)
        {
            _logger = logger;
            _userService = userService;
            _profileService = profileService;
			_achievementIssuingService = achievementIssuingService;

		}

		[Authorize(Policy = "user")]
		[HttpGet("GetProfiles")]
        public async Task<ActionResult<ErrorOr<List<User>>>> GetDateUsers(CancellationToken cancellationToken)
        {
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")?.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			var errorOrProfiles = await _profileService.GetAllProfilesAsync(cancellationToken);
            if (errorOrProfiles.IsError)
            {
                return BadRequest(errorOrProfiles.Errors);
            }

            return Ok(errorOrProfiles.Value.Where(profile => profile.Id != userId).ToList());
        }

		[Authorize(Policy = "user")]
		[HttpPost("UserActionLike")]
		public async Task<ActionResult<ErrorOr<Success>>> UserActionLike(
			Guid slaveId, 
			CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")?.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			Dictionary<Guid, (int LikesSent, int LikesGot, int DislikesSent, int DislikesGot)> userData = new();

			_cache.TryGetValue("Achievements", out userData);

			userData[userId] = (
				userData[userId].LikesSent + 1,
				userData[userId].LikesGot,
				userData[userId].DislikesSent,
				userData[userId].DislikesGot
				);

			userData[slaveId] = (
				userData[userId].LikesSent,
				userData[userId].LikesGot + 1,
				userData[userId].DislikesSent,
				userData[userId].DislikesGot
				);

			_cache.Set("Achievements", userData);
			await _achievementIssuingService.CheckUserAchievementsAsync(
				userId, cancellationToken);

			await _achievementIssuingService.CheckUserAchievementsAsync(
				slaveId, cancellationToken);
			return Ok();
		}

		[Authorize(Policy = "user")]
		[HttpPost("UserActionDislike")]
		public async Task<ActionResult<ErrorOr<Success>>> UserActionDislike(
			Guid slaveId,
			CancellationToken cancellationToken)
		{
			Guid userId;
			var flag = Guid.TryParse(HttpContext.User.FindFirstValue("UserId")?.ToString(), out userId);

			if (!flag)
			{
				return Unauthorized();
			}
			Dictionary<Guid, (int LikesSent, int LikesGot, int DislikesSent, int DislikesGot)> userData = new();

			_cache.TryGetValue("Achievements", out userData);

			userData[userId] = (
				userData[userId].LikesSent,
				userData[userId].LikesGot,
				userData[userId].DislikesSent + 1,
				userData[userId].DislikesGot
				);

			userData[slaveId] = (
				userData[userId].LikesSent,
				userData[userId].LikesGot,
				userData[userId].DislikesSent,
				userData[userId].DislikesGot + 1
				);

			_cache.Set("Achievements", userData);
			await _achievementIssuingService.CheckUserAchievementsAsync(
				userId, cancellationToken);

			await _achievementIssuingService.CheckUserAchievementsAsync(
				slaveId, cancellationToken);
			return Ok();
		}
	}
}
