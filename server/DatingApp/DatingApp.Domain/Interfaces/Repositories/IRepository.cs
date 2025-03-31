using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces.Repositories
{
	public interface IRepository<T>
	{
		Task<ErrorOr<Success>> AddAsync(T entity, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> UpdateAsync(T entity, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
		Task<ErrorOr<List<T>>> GetAllAsync(CancellationToken cancellationToken);
		Task<ErrorOr<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	}
}
