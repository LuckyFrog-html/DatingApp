
using DatingApp.Api.Extensions;
using DatingApp.Application.Extensions;
using DatingApp.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DatingApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			string connection = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
            
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApiAuthenticationAndAuthorization(builder.Configuration);
            builder.Services.AddRepos();
            builder.Services.AddDatingApp();
            builder.Services.AddSecurity();


			builder.WebHost.ConfigureKestrel(options => {
				options.ListenLocalhost(5000); // HTTP
				options.ListenLocalhost(5001, listenOptions => {
					listenOptions.UseHttps(); // HTTPS с самоподписанным сертификатом
				});
			});


			

			var app = builder.Build();
			app.UseHttpsRedirection();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
