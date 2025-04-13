using DatingApp.Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Data
{
	internal class CodeDbContext : DbContext
	{
		public DbSet<CodeConfirmation> CodeConfirmation => Set<CodeConfirmation>();

		public CodeDbContext(DbContextOptions<CodeDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CodeConfirmation>(entity =>
			{
				entity.HasIndex(c => c.Email).IsUnique();
				entity.Property(c => c.Code).IsRequired().HasMaxLength(8);
			});
		}
	}
}
