using DatingApp.Application.Core.Interfaces;
using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Application.Models.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Services
{
	public class ProfileService : IProfileService
	{
		private readonly IAchievementRepository _achievementRepository;
		private readonly IProfileRepository _profileRepository;
		private readonly IHobbyRepository _hobbyRepository;
		public ProfileService(IProfileRepository profileRepository,
			IHobbyRepository hobbyRepository,
			IAchievementRepository achievementRepository) 
		{
			_profileRepository = profileRepository;
			_hobbyRepository = hobbyRepository;
			_achievementRepository = achievementRepository;
		}
		public async Task<ErrorOr<Success>> AddAchievement(Guid userId, Achievement achievement, CancellationToken cancellationToken)
		{
			var profileResult = await _profileRepository.GetByIdAsync(userId, cancellationToken);
			if (profileResult.IsError)
			{
				return profileResult.Errors;
			}
			var profile = profileResult.Value;

			if (!profile.Achievements.Contains(achievement))
			{
				profile.Achievements.Add(achievement);
				profile.Balance += 10;
			}

			var result = await _profileRepository.UpdateAsync(profile, cancellationToken);

			if (result.IsError)
			{
				return result.Errors;
			}

			return result.Value;
		}

		public async Task<ErrorOr<Success>> CreateProfile(Guid userId, string name, int age,
			string town, bool gender, CancellationToken cancellationToken)
		{
			var ErrorOrProfile = await GetProfileByIdAsync(userId, cancellationToken);
			if (!ErrorOrProfile.IsError)
			{
				return Error.Conflict("Profile already exists");
			}

			var newProfile = new Profile
			{
				Id = userId,
				Name = name,
				Age = age,
				Town = town,
				Gender = gender,
			};

			await _profileRepository.AddAsync(newProfile, cancellationToken);

			var allAchievements = await _achievementRepository.GetAllAsync(cancellationToken);
			if (allAchievements.IsError)
			{
				return allAchievements.Errors;
			}

			Achievement days0 = allAchievements.Value.Find(achievement => achievement.Name == "0days");

			await AddAchievement(userId, days0, cancellationToken);

			return Result.Success;  
		}

		public async Task<ErrorOr<List<Profile>>> GetAllProfilesAsync(CancellationToken cancellationToken)
		{
			var errorOrProfiles = await _profileRepository.GetAllAsync(cancellationToken);
			if (errorOrProfiles.IsError)
			{
				return errorOrProfiles.Errors;
			}

			return errorOrProfiles.Value;
		}


		public async Task<ErrorOr<Success>> AddHobbyAsync(
			Guid userId, ICollection<string> addedHobbies, CancellationToken cancellationToken)
		{
			var profileResult = await _profileRepository.GetByIdAsync(userId, cancellationToken);
			if (profileResult.IsError)
			{
				return profileResult.Errors;
			}
			var profile = profileResult.Value;


			foreach (var elem in addedHobbies)
			{
				var hobbyResult = await _hobbyRepository.GetByNameAsync(elem, cancellationToken);
				if (hobbyResult.IsError)
				{
					return hobbyResult.Errors;
				}
				var hobby = hobbyResult.Value;

				if (!profile.Hobbies.Contains(hobby))
				{
					profile.Hobbies.Add(hobby);
				}
			}

			var result = await _profileRepository.UpdateAsync(profile, cancellationToken);

			if (result.IsError)
			{
				return result.Errors;
			}

			return result.Value;
		}

		public async Task<ErrorOr<ICollection<Hobby>>> GetHobbiesAsync(Guid userId, 
			CancellationToken cancellationToken)
		{
			var profileResult = await _profileRepository.GetByIdAsync(userId, cancellationToken);
			if (profileResult.IsError)
			{
				return profileResult.Errors;
			}
			var profile = profileResult.Value;

			return profile.Hobbies.ToList();
		}

		public async Task<ErrorOr<ICollection<Achievement>>> GetAchievements(Guid userId, CancellationToken cancellationToken)
		{
			var profileResult = await _profileRepository.GetByIdAsync(userId, cancellationToken);
			if (profileResult.IsError)
			{
				return profileResult.Errors;
			}
			var profile = profileResult.Value;

			return profile.Achievements.ToList();
		}

		public async Task<ErrorOr<Profile>> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken)
		{
			var profileResult = await _profileRepository.GetByIdAsync(userId, cancellationToken);
			if (profileResult.IsError)
			{
				return profileResult.Errors;
			}

			return profileResult.Value;
		}

		public async Task<ErrorOr<Success>> MarkAsDeletedAsync(Guid userId, CancellationToken cancellationToken)
		{
			var profileResult = await _profileRepository.GetByIdAsync(userId, cancellationToken);
			if (profileResult.IsError)
			{
				return profileResult.Errors;
			}

			var profile = profileResult.Value;
			profile.IsDeleted = true;
			return await _profileRepository.UpdateAsync(profile, cancellationToken); ;
		}

		public async Task<ErrorOr<Success>> EditProfile(Guid userId,
			ProfilePatchRequest updatedProfileInfo,
			CancellationToken cancellationToken)
		{
			var result = await GetProfileByIdAsync(userId, cancellationToken);

			if (result.IsError)
			{
				return result.Errors;
			}

			Profile userProfile = result.Value;
			userProfile.Name = updatedProfileInfo.Name;
			userProfile.Description = updatedProfileInfo.Description;
			userProfile.Age = updatedProfileInfo.Age;
			userProfile.Town = updatedProfileInfo.Town;



			return await _profileRepository.UpdateAsync(userProfile, cancellationToken);
		}
	}
}
