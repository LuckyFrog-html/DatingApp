using DatingApp.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IRoleRepository : IRepository<Role>
	{
		Task<ErrorOr<Role>> GetByNameAsync(string name, CancellationToken cancellationToken);
	}
}
