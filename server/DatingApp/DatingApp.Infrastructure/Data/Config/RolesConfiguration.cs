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

			builder.HasData(
			new Role
			{
				Id = Guid.Parse("3bcca34e-96f9-4587-9d40-9cb0debfcf5a"),
				Name = "user",
			},
			new Role
			{
				Id = Guid.Parse("8dde508d-6d11-4b9f-8db7-28eeb58117a6"),
				Name = "admin",
			},
			new Role
			{
				Id = Guid.Parse("d7775f8c-3e4d-4fc0-a51d-8736178b352f"),
				Name = "moderator"
			}
		);
		}
	}
}
