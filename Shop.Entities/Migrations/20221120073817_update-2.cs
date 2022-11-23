using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Entities.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 20, 14, 38, 16, 753, DateTimeKind.Local).AddTicks(6916), new DateTime(2022, 11, 20, 14, 38, 16, 753, DateTimeKind.Local).AddTicks(6908) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 20, 14, 38, 16, 753, DateTimeKind.Local).AddTicks(6920), new DateTime(2022, 11, 20, 14, 38, 16, 753, DateTimeKind.Local).AddTicks(6920) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 19, 13, 53, 1, 952, DateTimeKind.Local).AddTicks(4118), new DateTime(2022, 11, 19, 13, 53, 1, 952, DateTimeKind.Local).AddTicks(4109) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 19, 13, 53, 1, 952, DateTimeKind.Local).AddTicks(4121), new DateTime(2022, 11, 19, 13, 53, 1, 952, DateTimeKind.Local).AddTicks(4120) });
        }
    }
}
