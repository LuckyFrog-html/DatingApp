namespace DatingApp.Api.Extensions
{
	public static class OriginExtension
	{
		public static void AllowAllOrigins(this IServiceCollection services)
		{
			services.AddCors(options => {
				options.AddPolicy("AllowAllOrigins", builder => {
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});
		}
	}
}
