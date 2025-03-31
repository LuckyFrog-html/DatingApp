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
	public sealed class HobbiesConfiguration : IEntityTypeConfiguration<Hobby>
	{
		public void Configure(EntityTypeBuilder<Hobby> builder)
		{
			builder.ToTable("Hobbies");
			builder.HasKey(h => h.Id);
		}
	}
}
