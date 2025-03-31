using DatingApp.Application.Core.Interfaces;
using DatingApp.Application.Interfaces;
using DatingApp.Application.Models.Requests;
using DatingApp.Application.Models.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly IJWTProvider _jwtProvider;
		//private readonly IPasswordHasher _passwordHasher;

		public AuthService(IUserRepository userRepository,
			IRefreshTokenRepository refreshTokenRepository, 
			IJWTProvider jwtProvider)
		{
			_userRepository = userRepository;
			_refreshTokenRepository = refreshTokenRepository;
			_jwtProvider = jwtProvider;
		}

		public async Task<ErrorOr<LoginResponse>> LoginAsync(string username,
			string password, 
			CancellationToken cancellationToken)
		{
			var userResult = await _userRepository.GetByNameAsync(username, cancellationToken);
			if (userResult.IsError)
			{
				return userResult.Errors;
			}
			var user = userResult.Value;

			//if (!_passwordHasher.Verify(password, user.PasswordHash))
			//{
			//	return Error.Validation(description: "Invalid password");
			//}

			if (password != user.Password)
			{
				return Error.Validation(description: "Invalid password");
			}

			var refreshTokenResult = await _jwtProvider.GenerateRefreshToken(user, cancellationToken);
			if (refreshTokenResult.IsError) 
			{
				return refreshTokenResult.Errors;
			}
			var refreshToken = new RefreshToken
			{
				Value = refreshTokenResult.Value,
				Id = user.Id,
				User = user,
				CreatedAt = DateTime.UtcNow
			};

			await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

			var accessToken = await _jwtProvider.GenerateAccessToken(user, refreshToken.Value, cancellationToken);
			
			return new LoginResponse
				(
				AccessToken: accessToken.Value,
				RefreshToken: refreshToken.Value
				);
		}
	}
}
