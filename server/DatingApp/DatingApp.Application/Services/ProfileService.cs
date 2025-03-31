using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
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
		public Task<ErrorOr<Success>> AddAchievement(Guid userId, Achievement achievement, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<ICollection<Achievement>>> GetAchievements(Guid userId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
