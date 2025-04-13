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
	public sealed class ActionsConfiguration : IEntityTypeConfiguration<Domain.Entities.Action>
	{
		public void Configure(EntityTypeBuilder<Domain.Entities.Action> builder)
		{
			builder.ToTable("Actions");
			builder.HasKey(a => a.Id);
		}
	}
}
