using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Interfaces
{
	public interface IProfileService
	{
		public Task<ErrorOr<Success>> AddAchievement(Guid userId, Achievement achievement,
			CancellationToken cancellationToken);

		public Task<ErrorOr<ICollection<Achievement>>> GetAchievements(Guid userId, 
			CancellationToken cancellationToken);

		public Task<ErrorOr<Profile>> GetProfileByIdAsync(Guid userId,
			CancellationToken cancellationToken);

		public Task<ErrorOr<Success>> AddHobby(
			Guid userId, ICollection<string> hobbyName, CancellationToken cancellationToken);
		public Task<ErrorOr<ICollection<Hobby>>> GetHobbies(Guid userId,
			CancellationToken cancellationToken);

	}
}
