using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class autoAddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("24105e96-6df0-41fa-8a95-979e8d3c790d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a4b40ba2-6059-436c-830f-a1cbbd404693"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fcfdf370-ad97-4437-9a57-f9d56f96dcc0"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3bcca34e-96f9-4587-9d40-9cb0debfcf5a"), "user" },
                    { new Guid("8dde508d-6d11-4b9f-8db7-28eeb58117a6"), "admin" },
                    { new Guid("d7775f8c-3e4d-4fc0-a51d-8736178b352f"), "moderator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3bcca34e-96f9-4587-9d40-9cb0debfcf5a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8dde508d-6d11-4b9f-8db7-28eeb58117a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d7775f8c-3e4d-4fc0-a51d-8736178b352f"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("24105e96-6df0-41fa-8a95-979e8d3c790d"), "user" },
                    { new Guid("a4b40ba2-6059-436c-830f-a1cbbd404693"), "moderator" },
                    { new Guid("fcfdf370-ad97-4437-9a57-f9d56f96dcc0"), "admin" }
                });
        }
    }
}
