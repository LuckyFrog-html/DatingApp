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
	public class ProfileRepository : IProfileRepository
	{
		private readonly ApplicationContext _dbContext;

		public ProfileRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ErrorOr<Success>> AddAsync(Profile entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add profile. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new Profile { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete profile. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Profile>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var profiles = await _dbContext.Set<Profile>()
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return profiles;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get profiles. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Profile>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var profile = await _dbContext.Set<Profile>()
					.Include(p => p.Hobbies)
					.Include(p => p.Achievements)
					.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

				return profile is not null
					? profile
					: Error.NotFound(description: "Profile not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get profile. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Profile>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			try
			{
				var profile = await _dbContext.Set<Profile>()
					.Include(p => p.Hobbies)
					.Include(p => p.Achievements)
					.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

				return profile is not null
					? profile
					: Error.NotFound(description: "Profile not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByNameFailed", $"Failed to get profile. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(Profile entity, CancellationToken cancellationToken)
		{
			try
			{
				var exists = await _dbContext.Set<Profile>()
					.AnyAsync(p => p.Id == entity.Id, cancellationToken);

				if (!exists)
				{
					return Error.NotFound(description: "Profile not found");
				}

				_dbContext.Update(entity);
				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update profile. Error: {ex.Message}");
			}
		}
	}
}
