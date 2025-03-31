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
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationContext _dbContext;

		public UserRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ErrorOr<Success>> AddAsync(User entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add user. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new RefreshToken { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete user. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<User>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var tokens = await _dbContext.Set<User>()
					.ToListAsync(cancellationToken);

				return tokens;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get users. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var token = await _dbContext.Set<User>()
					.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

				return token is not null
					? token
					: Error.NotFound(description: "User not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get user. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<User>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _dbContext.Set<User>()
					.Include(u => u.Roles)
					.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);

				return user is not null
					? user
					: Error.NotFound(description: "User not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get user. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(User entity, CancellationToken cancellationToken)
		{
			try
			{
				var existingUser = await _dbContext.Set<User>()
					.AnyAsync(t => t.Id == entity.Id, cancellationToken);

				if (!existingUser)
				{
					return Error.NotFound(description: "User not found");
				}

				_dbContext.Update(entity);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update refresh token. Error: {ex.Message}");
			}
		}
	}
}
