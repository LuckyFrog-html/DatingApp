using DatingApp.Application.Models.Requests;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Interfaces
{
	public interface IUserService
	{
		Task<ErrorOr<Success>> CreateUserAsync(string email, string password, 
			CancellationToken cancellationToken);

		Task<ErrorOr<bool>> IsUserExists(string email, CancellationToken cancellationToken);

		Task<ErrorOr<List<User>>> GetAllUsersAsync(CancellationToken cancellationToken);

		Task<ErrorOr<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
	}
}
