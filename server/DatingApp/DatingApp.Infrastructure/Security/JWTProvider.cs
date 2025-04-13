using DatingApp.Application.Core.Interfaces;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces.Repositories;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Security
{
	internal class JWTProvider : IJWTProvider
	{
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		
		public JWTProvider(IConfiguration configuration,
			IRefreshTokenRepository refreshTokenRepository,
			IUserService userService) 
		{
			_configuration = configuration;
			_refreshTokenRepository = refreshTokenRepository;
			_userService = userService;
		}
		private async Task<bool> IsRefreshTokenExists(User user, string refreshToken, CancellationToken cancellationToken)
		{
			var tokensResult = await _refreshTokenRepository.GetAllAsync(cancellationToken);

			if (tokensResult.IsError)
			{
				return false;
			}

			List<RefreshToken> refreshTokens = tokensResult.Value;

			return refreshTokens.Any(x => x.Value == refreshToken && x.Id == user.Id);
		}

		public async Task<ErrorOr<string>> GenerateRefreshToken(User user, CancellationToken cancellationToken)
		{
			var refreshValue = Guid.NewGuid();
			return refreshValue.ToString();
		}

		public async Task<ErrorOr<string>> GenerateAccessToken(User user, string refreshToken, CancellationToken cancellationToken)
		{
			if (!await IsRefreshTokenExists(user, refreshToken, cancellationToken))
			{
				return Error.Validation("Refresh token has expired");
			}

			var jwtoptions = _configuration.GetRequiredSection("jwtoptions");

			List<Claim> claims = [new("UserId", user.Id.ToString())];

			foreach (var role in user.Roles)
			{
				claims.Add(new(ClaimTypes.Role, role.Name));
			}

				

			var secretKey = jwtoptions.GetRequiredSection("SecretKey").Value;
			var expiresAccessMinutes = jwtoptions.GetRequiredSection("ExpiresInMinutes").Value;

			var key = Encoding.UTF8.GetBytes(secretKey);
			var minutes = Convert.ToInt32(expiresAccessMinutes);

			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken
			(
				claims: claims,
				signingCredentials: signingCredentials,
				expires: DateTime.UtcNow.AddMinutes(minutes)
			);

			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenValue;
		}
	}
}
