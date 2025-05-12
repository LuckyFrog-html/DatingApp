using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using DatingApp.Application.Interfaces;

namespace DatingApp.Application.Services
{
	public class AchievementBackgroundService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<AchievementBackgroundService> _logger;

		public AchievementBackgroundService(
			IServiceProvider serviceProvider,
			ILogger<AchievementBackgroundService> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("AchievementBackgroundService is starting.");

			while (!cancellationToken.IsCancellationRequested)
			{

				

				var now = DateTime.UtcNow;
				var nextRun = now.Date.AddDays(1); // Следующий день в 00:00:00

				var delay = nextRun - now;
				_logger.LogInformation("Next run at {NextRun} (in {Delay})", nextRun, delay);

				await Task.Delay(delay, cancellationToken);

				try
				{
					using (var scope = _serviceProvider.CreateScope())
					{
						var achievementService = scope.ServiceProvider
							.GetRequiredService<IAchievementIssuingService>();

						await achievementService.CheckTimeAchievementAsync(cancellationToken);
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error in AchievementBackgroundService");
				}
			}
		}
	}
}
