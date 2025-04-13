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
	public sealed class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.ToTable("RefreshTokens");
			builder.HasKey(rt => new { rt.Id, rt.Value });

			builder.HasOne(rt => rt.User)
				.WithMany(u => u.RefreshTokens)
				.HasForeignKey(rt => rt.Id)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
