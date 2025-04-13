using DatingApp.Domain.Entities;
using System;
using ErrorOr;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<ErrorOr<User>> GetByNameAsync(string name, CancellationToken cancellationToken);
	}
}
