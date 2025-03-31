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
	public sealed class ProfilesConfiguration : IEntityTypeConfiguration<Profile>
	{
		public void Configure(EntityTypeBuilder<Profile> builder)
		{
			builder.ToTable("Profiles");
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
			   .ValueGeneratedNever();

			builder.HasOne(p => p.User)
				.WithOne(u => u.Profile)
				.HasForeignKey<User>(p => p.Id)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
