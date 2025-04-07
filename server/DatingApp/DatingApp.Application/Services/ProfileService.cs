using DatingApp.Application.Core.Interfaces;
using DatingApp.Application.Interfaces;
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
		private readonly IProfileRepository _profileRepository;
		private readonly IHobbyRepository _hobbyRepository;
		public ProfileService(IProfileRepository profileRepository,
			IHobbyRepository hobbyRepository) 
		{
			_profileRepository = profileRepository;
			_hobbyRepository = hobbyRepository;
		}
		public async Task<ErrorOr<Success>> AddAchievement(Guid userId, Achievement achievement, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task<ErrorOr<Success>> AddHobby(
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

		public async Task<ErrorOr<ICollection<Hobby>>> GetHobbies(Guid userId, 
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
	}
}
