using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.AuthAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17f82ea5-ff6d-4c8d-8abe-34e256f09fde", null, "Admin", "ADMINISTRATOR" },
                    { "8d15debb-3217-4e80-a76e-ce4f887ddf1c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17f82ea5-ff6d-4c8d-8abe-34e256f09fde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d15debb-3217-4e80-a76e-ce4f887ddf1c");
        }
    }
}
