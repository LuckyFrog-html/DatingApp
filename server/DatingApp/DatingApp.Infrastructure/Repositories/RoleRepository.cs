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
	public class RoleRepository : IRoleRepository
	{
		private readonly ApplicationContext _dbContext;

		public RoleRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ErrorOr<Success>> AddAsync(Role entity, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.AddAsync(entity, cancellationToken);
				await _dbContext.SaveChangesAsync(cancellationToken);
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add role. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var entity = new Role { Id = id };
				_dbContext.Attach(entity);
				_dbContext.Remove(entity);
				await _dbContext.SaveChangesAsync(cancellationToken);
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("DeleteFailed", $"Failed to delete role. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<List<Role>>> GetAllAsync(CancellationToken cancellationToken)
		{
			try
			{
				var roles = await _dbContext.Set<Role>()
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return roles;
			}
			catch (Exception ex)
			{
				return Error.Failure("GetAllFailed", $"Failed to get roles. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Role>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				var role = await _dbContext.Set<Role>()
					.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

				return role is not null
					? role
					: Error.NotFound(description: "Role not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetByIdFailed", $"Failed to get role. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Role>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			try
			{
				var role = await _dbContext.Set<Role>()
					.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);

				return role is not null
					? role
					: Error.NotFound(description: "Role not found");
			}
			catch (Exception ex)
			{
				return Error.Failure("GetRole", $"Failed to get role. Error: {ex.Message}");
			}
		}

		public async Task<ErrorOr<Success>> UpdateAsync(Role entity, CancellationToken cancellationToken)
		{
			try
			{
				var exists = await _dbContext.Set<Role>()
					.AnyAsync(r => r.Id == entity.Id, cancellationToken);

				if (!exists)
				{
					return Error.NotFound(description: "Role not found");
				}

				_dbContext.Update(entity);
				await _dbContext.SaveChangesAsync(cancellationToken);

				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("UpdateFailed", $"Failed to update role. Error: {ex.Message}");
			}
		}
	}
}
