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
		Task<ErrorOr<Success>> CreateUserAsync(RegisterRequest registerRequest, 
			CancellationToken cancellationToken);
	}
}
