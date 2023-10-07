using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"), "Ul Pushkina, dom kolotushkina", "Holiday Inn" },
                    { new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"), "Ul Kolotushkina, dom Pushkina", "Radisson Blue" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 35, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Kane Miller", "Administrator" },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 26, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sam Raiden", "Software developer" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 30, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Jana McLeaf", "Software developer" }
                });

            migrationBuilder.InsertData(
                table: "Lodgers",
                columns: new[] { "LodgerId", "HotelId", "Name", "Room" },
                values: new object[,]
                {
                    { new Guid("49258e36-100f-4bdb-9cd0-cb5db876e903"), new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"), "Pivkin Aleksey", 0L },
                    { new Guid("ac963d4c-0383-4f52-9a64-2132be68b39b"), new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"), "Potapov Nikita", 0L },
                    { new Guid("cf757914-e4ef-4515-a5c2-ec0b3744ffc2"), new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"), "Ovtin Ruslan", 0L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Lodgers",
                keyColumn: "LodgerId",
                keyValue: new Guid("49258e36-100f-4bdb-9cd0-cb5db876e903"));

            migrationBuilder.DeleteData(
                table: "Lodgers",
                keyColumn: "LodgerId",
                keyValue: new Guid("ac963d4c-0383-4f52-9a64-2132be68b39b"));

            migrationBuilder.DeleteData(
                table: "Lodgers",
                keyColumn: "LodgerId",
                keyValue: new Guid("cf757914-e4ef-4515-a5c2-ec0b3744ffc2"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"));
        }
    }
}
