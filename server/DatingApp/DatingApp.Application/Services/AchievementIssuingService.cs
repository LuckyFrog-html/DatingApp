using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Services
{
	public class AchievementIssuingService : IAchievementIssuingService
	{
		private readonly IAchievementRepository _achievementRepo;
		private readonly IProfileService _profileService;
		private readonly IUserService _userService;
		private readonly IMemoryCache _cache;

		public AchievementIssuingService(IAchievementRepository achievementRepo,
			IUserService userService,
			IMemoryCache cache,
			IProfileService profileService)
		{
			_achievementRepo = achievementRepo;
			_userService = userService;
			_cache = cache;
			_profileService = profileService;
		}

		public async Task<ErrorOr<Success>> CheckUserAchievementsAsync(Guid userId, CancellationToken cancellationToken)
		{
			var allAchievements = await _achievementRepo.GetAllAsync(cancellationToken);
			if (allAchievements.IsError)
			{
				return allAchievements.Errors;
			}

			Dictionary<Guid, (int LikesSent, int LikesGot, int DislikesSent, int DislikesGot)> userAchievementData = new();

			if (!_cache.TryGetValue("Achievements", out userAchievementData))
			{
				await SetAchievemntCache(cancellationToken);
			}

			_cache.TryGetValue("Achievements", out userAchievementData);

			Achievement likesent1 = allAchievements.Value.Find(achievement => achievement.Name == "1likesent");
			Achievement likesent100 = allAchievements.Value.Find(achievement => achievement.Name == "100likesent");

			Achievement dislikesent1 = allAchievements.Value.Find(achievement => achievement.Name == "1dislikesent");
			Achievement dislikesent100 = allAchievements.Value.Find(achievement => achievement.Name == "100dislikesent");

			Achievement likegot1 = allAchievements.Value.Find(achievement => achievement.Name == "1likegot");
			Achievement likegot100 = allAchievements.Value.Find(achievement => achievement.Name == "100likegot");

			Achievement dislikegot1 = allAchievements.Value.Find(achievement => achievement.Name == "1dislikegot");
				Achievement dislikegot100 = allAchievements.Value.Find(achievement => achievement.Name == "100dislikegot");

			var userInfo = userAchievementData[userId];
			if (userInfo.LikesSent >= 100)
			{
				await _profileService.AddAchievement(userId, likesent100, cancellationToken);
			}

			if (userInfo.LikesSent >= 1)
			{
				await _profileService.AddAchievement(userId, likesent1, cancellationToken);
			}
			if (userInfo.DislikesSent >= 100)
			{
				await _profileService.AddAchievement(userId, dislikesent100, cancellationToken);
			}
			if (userInfo.DislikesSent >= 1)
			{
				await _profileService.AddAchievement(userId, dislikesent1, cancellationToken);
			}

			if (userInfo.LikesGot >= 100)
			{
				await _profileService.AddAchievement(userId, likegot100, cancellationToken);
			}
			if (userInfo.LikesGot >= 1)
			{
				await _profileService.AddAchievement(userId, likegot1, cancellationToken);
			}

			if (userInfo.DislikesGot >= 100)
			{
				await _profileService.AddAchievement(userId, dislikegot100, cancellationToken);
			}
			if (userInfo.DislikesGot >= 1)
			{
				await _profileService.AddAchievement(userId, dislikegot1, cancellationToken);
			}

			return Result.Success;
		}

		public async Task<ErrorOr<Success>> CheckAllUserAchievementsAsync(CancellationToken cancellationToken)
		{
			var usersOrError = await _userService.GetAllUsersAsync(cancellationToken);
			if (usersOrError.IsError)
			{
				return usersOrError.Errors;
			}

			foreach (var user in usersOrError.Value)
			{
				await CheckUserAchievementsAsync(user.Id, cancellationToken);
			}

			return Result.Success;
		}

		public async Task<ErrorOr<Success>> CheckTimeAchievementAsync(CancellationToken cancellationToken)
		{
			var allAchievementsOrError = await _achievementRepo.GetAllAsync(cancellationToken);
			if (allAchievementsOrError.IsError)
			{
				return allAchievementsOrError.Errors;
			}
			var allAchievements = allAchievementsOrError.Value;

			var usersOrError = await _userService.GetAllUsersAsync(cancellationToken);
			if (usersOrError.IsError)
			{
				return usersOrError.Errors;
			}

			var profilesOrError = await _profileService.GetAllProfilesAsync(cancellationToken);
			if (profilesOrError.IsError)
			{
				return profilesOrError.Errors;
			}

			Achievement day365 = allAchievements.Find(achievement => achievement.Name == "365days");
			Achievement day30 = allAchievements.Find(achievement => achievement.Name == "30days");

			DateTime dateTime = DateTime.UtcNow;

			foreach (var profile in profilesOrError.Value)
			{
				var user = profile.User;
				var errorOrAchievements = await _profileService.GetAchievements(user.Id, cancellationToken);
				if (errorOrAchievements.IsError)
				{
					return errorOrAchievements.Errors;
				}



				TimeSpan userAge = dateTime - user.CreatedAt;
				if (userAge > TimeSpan.FromDays(365))
				{
					await _profileService.AddAchievement(user.Id, day365, cancellationToken);
				}
				if (userAge > TimeSpan.FromDays(30))
				{
					await _profileService.AddAchievement(user.Id, day30, cancellationToken);
				}
			}
			return Result.Success;
		}

		private async Task<ErrorOr<Success>> SetAchievemntCache(CancellationToken cancellationToken)
		{
			var usersOrError = await _userService.GetAllUsersAsync(cancellationToken);
			if (usersOrError.IsError)
			{
				return usersOrError.Errors;
			}
			var userAchievementsData = new Dictionary<Guid, (int LikesSent, int LikesGot, int DislikesSent, int DislikesGot)>();

			foreach (var user in usersOrError.Value)
			{
				userAchievementsData.Add(user.Id, (0, 0, 0, 0));
			}

			_cache.Set("Achievements", userAchievementsData);

			return Result.Success;
		}
	}
}
