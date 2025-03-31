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
	public sealed class PredictionsConfiguration : IEntityTypeConfiguration<Prediction>
	{
		public void Configure(EntityTypeBuilder<Prediction> builder)
		{
			builder.ToTable("Predictions");
			builder.HasKey(p => p.Id);
		}
	}
}
