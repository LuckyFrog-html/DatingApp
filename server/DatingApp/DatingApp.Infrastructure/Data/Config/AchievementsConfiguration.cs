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
	public sealed class AchievementsConfiguration : IEntityTypeConfiguration<Achievement>
	{
		public void Configure(EntityTypeBuilder<Achievement> builder)
		{
			builder.ToTable("Achievements");
			builder.HasKey(x => x.Id);
		}
	}
}
