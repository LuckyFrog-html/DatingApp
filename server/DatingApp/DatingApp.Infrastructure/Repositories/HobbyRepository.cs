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
	public class HobbyRepository : IHobbyRepository
	{
		private readonly ApplicationContext _dbContext;

		public HobbyRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ErrorOr<Success>> AddAsync(Hobby entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				await _dbContext.SaveChangesAsync();
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add hobby. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new Hobby { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);
				await _dbContext.SaveChangesAsync(cancellationToken);
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete hobby. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Hobby>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var hobbies = await _dbContext.Set<Hobby>()
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return hobbies;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get hobbies. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Hobby>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var hobby = await _dbContext.Set<Hobby>()
					.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

				return hobby is not null
					? hobby
					: Error.NotFound(description: "Hobby not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get hobby. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Hobby>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			try
			{
				var hobby = await _dbContext.Set<Hobby>()
					.FirstOrDefaultAsync(h => h.Name == name, cancellationToken);

				return hobby is not null
					? hobby
					: Error.NotFound(description: "Hobby not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByNameFailed", $"Failed to get hobby. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(Hobby entity, CancellationToken cancellationToken)
		{
			try
			{
				var exists = await _dbContext.Set<Hobby>()
					.AnyAsync(h => h.Id == entity.Id, cancellationToken);

				if (!exists)
				{
					return Error.NotFound(description: "Hobby not found");
				}

				_dbContext.Update(entity);
				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update hobby. Error: {ex.Message}");
			}
		}
	}
}
