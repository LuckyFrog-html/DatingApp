using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoSomething1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Profiles_ProfileId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_Profiles_ProfileId",
                table: "Hobbies");

            migrationBuilder.DropIndex(
                name: "IX_Hobbies_ProfileId",
                table: "Hobbies");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_ProfileId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Achievements");

            migrationBuilder.CreateTable(
                name: "AchievementProfile",
                columns: table => new
                {
                    AchievementsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementProfile", x => new { x.AchievementsId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_AchievementProfile_Achievements_AchievementsId",
                        column: x => x.AchievementsId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementProfile_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HobbyProfile",
                columns: table => new
                {
                    HobbiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyProfile", x => new { x.HobbiesId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_HobbyProfile_Hobbies_HobbiesId",
                        column: x => x.HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyProfile_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementProfile_ProfileId",
                table: "AchievementProfile",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbyProfile_ProfileId",
                table: "HobbyProfile",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementProfile");

            migrationBuilder.DropTable(
                name: "HobbyProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Hobbies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Achievements",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("13de6946-df16-45e0-b3ef-151fed66dd4c"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("1ddd25d2-3813-4301-b7a7-91ce30d79666"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("29692dab-526b-46ee-a76b-b0e1bfb11a85"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("55d18c79-209c-45b9-892a-d83926ddbfca"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("5c613232-a6ca-4ee5-89b7-7d82c4a9fb3d"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("65c7a89d-eb8a-495a-b165-837a696472c8"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("8ea9c875-a26b-40da-99c3-9a7ae084ef0a"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("a8d62703-b2a3-4141-abbc-276c62829969"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("c0b8f363-47de-46a1-a0b5-b927f6e0f37a"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("d395bac9-476c-4d1c-893c-59ac72f3bfc3"),
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("dfe6a248-d9f3-4d24-ba92-e5bd51d636b8"),
                column: "ProfileId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_ProfileId",
                table: "Hobbies",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_ProfileId",
                table: "Achievements",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Profiles_ProfileId",
                table: "Achievements",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_Profiles_ProfileId",
                table: "Hobbies",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }
    }
}
