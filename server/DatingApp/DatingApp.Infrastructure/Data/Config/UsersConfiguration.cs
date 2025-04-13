using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Config
{
	public sealed class UsersConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(u => u.Id);

			builder.HasMany(u => u.Roles)
				.WithMany(r => r.Users)
				.UsingEntity(j => j.ToTable("Users_Roles"));
		}
	}
}
