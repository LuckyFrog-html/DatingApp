using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Api.Extensions
{
	public static class AuthenticationExtentions
	{
		public static void AddApiAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
		{

			var key = Encoding.ASCII.GetBytes(configuration.GetRequiredSection("jwtoptions:SecretKey").Value!);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};

				x.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["X-Access-Token"];

						return Task.CompletedTask;
					}
				};
			});

			services.AddAuthorization(

				options =>
				{
					options.AddPolicy("admin",
						policy =>
						{
							policy.RequireRole("admin");

						});

					options.AddPolicy("user",
						policy =>
						{
							policy.RequireRole("admin", "user");

						});

					options.AddPolicy("moderator",
						policy =>
						{
							policy.RequireRole("moderator", "user");
						});
				}
			 );
		}

	}
}
