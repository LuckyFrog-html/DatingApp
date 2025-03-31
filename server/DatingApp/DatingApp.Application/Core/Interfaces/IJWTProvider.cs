using DatingApp.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Core.Interfaces
{
	public interface IJWTProvider
	{
		public Task<ErrorOr<string>> GenerateRefreshToken(User user, CancellationToken cancellationToken);
		public Task<ErrorOr<string>> GenerateAccessToken(User user, string refreshToken, CancellationToken cancellationToken);
	}
}
