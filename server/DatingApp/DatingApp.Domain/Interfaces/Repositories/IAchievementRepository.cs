using DatingApp.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IAchievementRepository : IRepository<Achievement>
	{
		Task<ErrorOr<Achievement>> GetByNameAsync(string name, CancellationToken cancellationToken);
	}
}
