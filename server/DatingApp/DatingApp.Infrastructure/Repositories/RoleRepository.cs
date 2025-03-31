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

		public Task<ErrorOr<Success>> AddAsync(Role entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<List<Role>>> GetAllAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<Role>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
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

		public Task<ErrorOr<Success>> UpdateAsync(Role entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
