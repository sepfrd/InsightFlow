#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace RedditMockup.DataAccess.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6770), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6770) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6780), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6780) });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Bio", "CreationDate", "Email", "LastUpdated", "UserId" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110), "", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110), 1 },
                    { 2, "", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110), "", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110), 2 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6700), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6750), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7120), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7130), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6800), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6800) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7060), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7060) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3270), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3270), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3180), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3220), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3580), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3580), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3290), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3530), new DateTime(2022, 7, 12, 2, 33, 23, 707, DateTimeKind.Local).AddTicks(3530) });
        }
    }
}
