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
		public Task<ErrorOr<Success>> AddAsync(Hobby entity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
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
