using DatingApp.Domain.Entities;
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
	}
}
