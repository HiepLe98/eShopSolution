using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 18, 10, 45, 57, 77, DateTimeKind.Local).AddTicks(3217),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 18, 10, 41, 34, 304, DateTimeKind.Local).AddTicks(7959));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4acadfe9-e895-4fe9-bb7f-8dfec73e3999");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5f23db50-4969-447e-9353-04d49b4cf1d4", "AQAAAAEAACcQAAAAEB9Vi/qWKri/RROyDwb6ibJ/tHuNmtm/ql/WkSna1eMQDgMkzi5IzFLEfjVHLfUmsQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "ModifiedDateTime" },
                values: new object[] { new DateTime(2020, 12, 18, 10, 45, 57, 96, DateTimeKind.Local).AddTicks(4869), new DateTime(2020, 12, 18, 10, 45, 57, 96, DateTimeKind.Local).AddTicks(5347) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 18, 10, 41, 34, 304, DateTimeKind.Local).AddTicks(7959),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 18, 10, 45, 57, 77, DateTimeKind.Local).AddTicks(3217));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e3cf517f-55ad-48f4-9d07-8e255b1599a2");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98d1e0ae-35d2-477e-99ca-ced18b121110", "AQAAAAEAACcQAAAAEOkxx2m+O2Ss1hWz8wW0Wa7cRncQdR5zNqUzGC55Go/SnrVEoURWs4GyUu7EQqcy9g==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "ModifiedDateTime" },
                values: new object[] { new DateTime(2020, 12, 18, 10, 41, 34, 324, DateTimeKind.Local).AddTicks(7618), new DateTime(2020, 12, 18, 10, 41, 34, 324, DateTimeKind.Local).AddTicks(8108) });
        }
    }
}
