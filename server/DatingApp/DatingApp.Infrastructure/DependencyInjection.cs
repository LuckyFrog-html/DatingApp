using DatingApp.Application.Core.Interfaces;
using DatingApp.Domain.Interfaces.Repositories;
using DatingApp.Infrastructure.Persistence;
using DatingApp.Infrastructure.Repositories;
using DatingApp.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddSecurity(this IServiceCollection services)
		{
			//services.AddTransient<IPasswordHasher, PasswordHasher>();
			services.AddTransient<IJWTProvider, JWTProvider>();

			return services;
		}

		public static IServiceCollection AddRepos(this IServiceCollection services)
		{
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IProfileRepository, ProfileRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
