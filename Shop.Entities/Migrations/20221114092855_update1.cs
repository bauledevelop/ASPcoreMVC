using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Entities.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "small",
                table: "Files",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "datetime2",
                table: "Files",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Files",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 14, 16, 28, 54, 153, DateTimeKind.Local).AddTicks(9972), new DateTime(2022, 11, 14, 16, 28, 54, 152, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 14, 16, 28, 54, 154, DateTimeKind.Local).AddTicks(3026), new DateTime(2022, 11, 14, 16, 28, 54, 154, DateTimeKind.Local).AddTicks(3016) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Files",
                newName: "datetime2");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Files",
                newName: "small");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 14, 1, 56, 27, 343, DateTimeKind.Local).AddTicks(1253), new DateTime(2022, 11, 14, 1, 56, 27, 341, DateTimeKind.Local).AddTicks(6770) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 14, 1, 56, 27, 343, DateTimeKind.Local).AddTicks(4489), new DateTime(2022, 11, 14, 1, 56, 27, 343, DateTimeKind.Local).AddTicks(4481) });
        }
    }
}
