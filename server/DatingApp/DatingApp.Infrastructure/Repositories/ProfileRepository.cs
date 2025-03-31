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

		public Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<List<Profile>>> GetAllAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<Profile>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<User>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<Success>> UpdateAsync(Profile entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
