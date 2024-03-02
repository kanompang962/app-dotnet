using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace app_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class SeddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b18e0cef-783e-44c4-9470-8e4d4bf3ffa3", null, "User", "USER" },
                    { "caf3ddcd-d7f5-4f06-a584-e740311b07ec", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b18e0cef-783e-44c4-9470-8e4d4bf3ffa3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caf3ddcd-d7f5-4f06-a584-e740311b07ec");
        }
    }
}
