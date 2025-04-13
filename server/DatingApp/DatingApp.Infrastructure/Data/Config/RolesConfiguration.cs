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
	public sealed class RolesConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles");
			builder.HasKey(r => r.Id);
			
			builder.HasMany(r => r.Users)
				.WithMany(u => u.Roles)
				.UsingEntity(j => j.ToTable("Users_Roles"));
		}
	}
}
