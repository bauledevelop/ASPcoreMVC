using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Entities.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Slides",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 12, 15, 0, 55, 27, 958, DateTimeKind.Local).AddTicks(8306), new DateTime(2022, 12, 15, 0, 55, 27, 958, DateTimeKind.Local).AddTicks(8297) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 12, 15, 0, 55, 27, 958, DateTimeKind.Local).AddTicks(8311), new DateTime(2022, 12, 15, 0, 55, 27, 958, DateTimeKind.Local).AddTicks(8310) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Slides",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 12, 15, 0, 14, 57, 667, DateTimeKind.Local).AddTicks(2692), new DateTime(2022, 12, 15, 0, 14, 57, 667, DateTimeKind.Local).AddTicks(2681) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 12, 15, 0, 14, 57, 667, DateTimeKind.Local).AddTicks(2698), new DateTime(2022, 12, 15, 0, 14, 57, 667, DateTimeKind.Local).AddTicks(2697) });
        }
    }
}
