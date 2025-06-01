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
	public class ActionRepository : IActionRepository
	{
		private readonly ApplicationContext _dbContext;

		public ActionRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ErrorOr<Success>> AddAsync(Domain.Entities.Action entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				await _dbContext.SaveChangesAsync();
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add action. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new Domain.Entities.Action { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete action. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Domain.Entities.Action>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var achievements = await _dbContext.Set<Domain.Entities.Action>()
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return achievements;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get actions. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Domain.Entities.Action>>> GetByMasterIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var achievement = await _dbContext.Set<Domain.Entities.Action>()
					.AsNoTracking()
					.Where(t => t.MasterId == id)
					.ToListAsync();

				return achievement is not null
					? achievement
					: Error.NotFound(description: "Action not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get Action. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Domain.Entities.Action>>> GetBySlaveIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var achievement = await _dbContext.Set<Domain.Entities.Action>()
					.Where(t => t.SlaveId == id)
					.ToListAsync();

				return achievement is not null
					? achievement
					: Error.NotFound(description: "Action not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get Action. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Domain.Entities.Action>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			//only one
			try
			{
				var achievement = await _dbContext.Set<Domain.Entities.Action>()
					.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);

				return achievement is not null
					? achievement
					: Error.NotFound(description: "action not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByNameFailed", $"Failed to get action. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(Domain.Entities.Action entity, CancellationToken cancellationToken)
		{
			try
			{
				var existingAchievemnt = await _dbContext.Set<Domain.Entities.Action>()
					.AnyAsync(t => t.Id == entity.Id, cancellationToken);

				if (!existingAchievemnt)
				{
					return Error.NotFound(description: "Action not found");
				}

				_dbContext.Update(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update action. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Domain.Entities.Action>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var achievement = await _dbContext.Set<Domain.Entities.Action>()
					.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

				return achievement is not null
					? achievement
					: Error.NotFound(description: "Action not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get Action. Error: {ex.Message}");
			}
		}
	}
}
