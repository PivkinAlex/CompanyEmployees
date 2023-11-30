using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "025905ee-a785-485f-aab8-242f4a7025af", "70a96216-0363-47cb-868c-1111ed0d92fb", "Administrator", "ADMINISTRATOR" },
                    { "ddc402c4-bbad-461e-89be-c02a7d1e5ebb", "9fd11353-ef53-4461-8fbc-155927c1b11a", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "025905ee-a785-485f-aab8-242f4a7025af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddc402c4-bbad-461e-89be-c02a7d1e5ebb");
        }
    }
}
