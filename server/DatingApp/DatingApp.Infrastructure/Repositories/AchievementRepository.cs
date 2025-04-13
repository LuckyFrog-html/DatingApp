using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Repositories
{
	public class AchievementRepository : IAchievementRepository
	{
		private readonly ApplicationContext _dbContext;

		public AchievementRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ErrorOr<Success>> AddAsync(Achievement entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				await _dbContext.SaveChangesAsync();
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add achievement. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new Achievement { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete achievement. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Achievement>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var achievements = await _dbContext.Set<Achievement>()
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return achievements;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get achievement. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Achievement>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var achievement = await _dbContext.Set<Achievement>()
					.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

				return achievement is not null
					? achievement
					: Error.NotFound(description: "Achievement not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get achievement. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Achievement>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			try
			{
				var achievement = await _dbContext.Set<Achievement>()
					.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);

				return achievement is not null
					? achievement
					: Error.NotFound(description: "Achievement not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByNameFailed", $"Failed to get achievement. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(Achievement entity, CancellationToken cancellationToken)
		{
			try
			{
				var existingAchievemnt = await _dbContext.Set<Achievement>()
					.AnyAsync(t => t.Id == entity.Id, cancellationToken);

				if (!existingAchievemnt)
				{
					return Error.NotFound(description: "Achievement not found");
				}

				_dbContext.Update(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update achievement. Error: {ex.Message}");
			}
		}
	}
}
