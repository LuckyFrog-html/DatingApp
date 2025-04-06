using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
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
				return new Success();
			}
			catch (Exception ex)
			{
				return Error.Failure("AddFailed", $"Failed to add hobby. Error: {ex.Message}");
			}
		}

		public Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<List<Hobby>>> GetAllAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<Hobby>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<User>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ErrorOr<Success>> UpdateAsync(Hobby entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
