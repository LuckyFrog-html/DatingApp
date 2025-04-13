using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IUnitOfWork
	{
		Task BeginTransactionAsync(CancellationToken cancellationToken);
		Task CommitAsync(CancellationToken cancellationToken);
		Task RollbackAsync(CancellationToken cancellationToken);
		Task SaveChangesAsync(CancellationToken cancellationToken);
	}
}
