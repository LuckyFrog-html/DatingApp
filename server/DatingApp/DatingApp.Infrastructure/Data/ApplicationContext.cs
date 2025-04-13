using DatingApp.Domain.Entities;
using DatingApp.Domain;
using DatingApp.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Achievement> Achievements { get; set; } = null!;
		public DbSet<Domain.Entities.Action> Actions { get; set; } = null!;
		public DbSet<Hobby> Hobbies { get; set;} = null!;
		public DbSet<Prediction> Predictions { get; set; } = null!;
		public DbSet<Profile> Profiles { get; set; } = null!;
		public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
		public DbSet<Role> Roles { get; set; } = null!;
		public DbSet<User> Users { get; set; } = null!;
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.ApplyConfiguration(new AchievementsConfiguration());
			modelBuilder.ApplyConfiguration(new ActionsConfiguration());
			modelBuilder.ApplyConfiguration(new HobbiesConfiguration());
			modelBuilder.ApplyConfiguration(new PredictionsConfiguration());
			modelBuilder.ApplyConfiguration(new ProfilesConfiguration());
			modelBuilder.ApplyConfiguration(new RefreshTokensConfiguration());
			modelBuilder.ApplyConfiguration(new RolesConfiguration());
			modelBuilder.ApplyConfiguration(new UsersConfiguration());
		}
	}
}
