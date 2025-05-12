using DatingApp.Domain.Entities;
using ErrorOr;

namespace DatingApp.Application.Interfaces
{
	public interface IAchievementIssuingService
	{
		Task<ErrorOr<Success>> CheckAllUserAchievementsAsync(CancellationToken cancellationToken);
		Task<ErrorOr<Success>> CheckTimeAchievementAsync(CancellationToken cancellationToken);
		Task<ErrorOr<Success>> CheckUserAchievementsAsync(Guid userId, CancellationToken cancellationToken);
	}
}