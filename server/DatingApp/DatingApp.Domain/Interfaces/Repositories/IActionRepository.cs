using DatingApp.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IActionRepository : IRepository<Entities.Action>
	{
		Task<ErrorOr<User>> GetByNameAsync(string name, CancellationToken cancellationToken);
		Task<ErrorOr<User>> GetByOwnerIdAsync(Guid guid, CancellationToken cancellationToken);
		Task<ErrorOr<User>> GetBySlaveIdAsync(Guid guid, CancellationToken cancellationToken);

	}
}
