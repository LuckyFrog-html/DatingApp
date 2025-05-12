using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoSomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Description", "Name", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("13de6946-df16-45e0-b3ef-151fed66dd4c"), "Поставить 100 дизлайков", "100dislikesent", null },
                    { new Guid("1ddd25d2-3813-4301-b7a7-91ce30d79666"), "Стаж 1 месяц", "30days", null },
                    { new Guid("29692dab-526b-46ee-a76b-b0e1bfb11a85"), "Стаж 1 год", "365days", null },
                    { new Guid("55d18c79-209c-45b9-892a-d83926ddbfca"), "Получить 100 дизлайков", "100dislikegot", null },
                    { new Guid("5c613232-a6ca-4ee5-89b7-7d82c4a9fb3d"), "Поставить первый лайк", "1likesent", null },
                    { new Guid("65c7a89d-eb8a-495a-b165-837a696472c8"), "Получить первый лайк", "1likegot", null },
                    { new Guid("8ea9c875-a26b-40da-99c3-9a7ae084ef0a"), "Поставить первый дизлайк", "1dislikesent", null },
                    { new Guid("a8d62703-b2a3-4141-abbc-276c62829969"), "Получить первый дизлайк", "1dislikegot", null },
                    { new Guid("c0b8f363-47de-46a1-a0b5-b927f6e0f37a"), "Получить 100 лайков", "100likegot", null },
                    { new Guid("d395bac9-476c-4d1c-893c-59ac72f3bfc3"), "Успешная регистрация в сервисе", "0days", null },
                    { new Guid("dfe6a248-d9f3-4d24-ba92-e5bd51d636b8"), "Поставить 100 лайков", "100likesent", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("13de6946-df16-45e0-b3ef-151fed66dd4c"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("1ddd25d2-3813-4301-b7a7-91ce30d79666"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("29692dab-526b-46ee-a76b-b0e1bfb11a85"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("55d18c79-209c-45b9-892a-d83926ddbfca"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("5c613232-a6ca-4ee5-89b7-7d82c4a9fb3d"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("65c7a89d-eb8a-495a-b165-837a696472c8"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("8ea9c875-a26b-40da-99c3-9a7ae084ef0a"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("a8d62703-b2a3-4141-abbc-276c62829969"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("c0b8f363-47de-46a1-a0b5-b927f6e0f37a"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("d395bac9-476c-4d1c-893c-59ac72f3bfc3"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("dfe6a248-d9f3-4d24-ba92-e5bd51d636b8"));
        }
    }
}
