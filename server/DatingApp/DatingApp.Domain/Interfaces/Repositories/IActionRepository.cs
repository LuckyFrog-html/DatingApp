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
		Task<ErrorOr<Domain.Entities.Action>> GetByNameAsync(string name, CancellationToken cancellationToken);
		Task<ErrorOr<List<Domain.Entities.Action>>> GetByMasterIdAsync(Guid guid, CancellationToken cancellationToken);
		Task<ErrorOr<List<Domain.Entities.Action>>> GetBySlaveIdAsync(Guid guid, CancellationToken cancellationToken);

	}
}
