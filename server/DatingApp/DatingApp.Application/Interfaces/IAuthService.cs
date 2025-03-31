using DatingApp.Application.Models.Requests;
using DatingApp.Application.Models.Responses;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Interfaces
{
	public interface IAuthService
	{
		public Task<ErrorOr<LoginResponse>> LoginAsync(
			string email,
			string password,
			CancellationToken cancellationToken);
	}
}
