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

			builder.HasData(
				new Achievement
				{
					Id = Guid.Parse("d395bac9-476c-4d1c-893c-59ac72f3bfc3"),
					Name = "0days",
					Description = "Успешная регистрация в сервисе"
				},
				new Achievement
				{
					Id = Guid.Parse("1ddd25d2-3813-4301-b7a7-91ce30d79666"),
					Name = "30days",
					Description = "Стаж 1 месяц"
				},
				new Achievement
				{
					Id = Guid.Parse("29692dab-526b-46ee-a76b-b0e1bfb11a85"),
					Name = "365days",
					Description = "Стаж 1 год"
				},


				new Achievement
				{
					Id = Guid.Parse("5c613232-a6ca-4ee5-89b7-7d82c4a9fb3d"),
					Name = "1likesent",
					Description = "Поставить первый лайк"
				},
				new Achievement
				{
					Id = Guid.Parse("dfe6a248-d9f3-4d24-ba92-e5bd51d636b8"),
					Name = "100likesent",
					Description = "Поставить 100 лайков"
				},



				new Achievement
				{
					Id = Guid.Parse("8ea9c875-a26b-40da-99c3-9a7ae084ef0a"),
					Name = "1dislikesent",
					Description = "Поставить первый дизлайк"
				},
				new Achievement
				{
					Id = Guid.Parse("13de6946-df16-45e0-b3ef-151fed66dd4c"),
					Name = "100dislikesent",
					Description = "Поставить 100 дизлайков"
				},


				new Achievement
				{
					Id = Guid.Parse("65c7a89d-eb8a-495a-b165-837a696472c8"),
					Name = "1likegot",
					Description = "Получить первый лайк"
				},
				new Achievement
				{
					Id = Guid.Parse("c0b8f363-47de-46a1-a0b5-b927f6e0f37a"),
					Name = "100likegot",
					Description = "Получить 100 лайков"
				},


				new Achievement
				{
					Id = Guid.Parse("a8d62703-b2a3-4141-abbc-276c62829969"),
					Name = "1dislikegot",
					Description = "Получить первый дизлайк"
				},
				new Achievement
				{
					Id = Guid.Parse("55d18c79-209c-45b9-892a-d83926ddbfca"),
					Name = "100dislikegot",
					Description = "Получить 100 дизлайков"
				}
			);
		}
	}
}
