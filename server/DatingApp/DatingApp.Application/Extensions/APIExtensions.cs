using DatingApp.Application.Interfaces;
using DatingApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Extensions
{
	public static class APIExtentions
	{
		public static void AddDatingApp(this IServiceCollection services)
		{
			services.AddScoped<IProfileService, ProfileService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			
		}
	}
}
