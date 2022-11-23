using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Entities.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Products",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rate = table.Column<int>(type: "int", nullable: false),
                    IDProduct = table.Column<long>(type: "bigint", nullable: false),
                    IDAccount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rates_Accounts_IDAccount",
                        column: x => x.IDAccount,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rates_Products_IDProduct",
                        column: x => x.IDProduct,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 19, 2, 35, 5, 623, DateTimeKind.Local).AddTicks(7752), new DateTime(2022, 11, 19, 2, 35, 5, 623, DateTimeKind.Local).AddTicks(7743) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 19, 2, 35, 5, 623, DateTimeKind.Local).AddTicks(7756), new DateTime(2022, 11, 19, 2, 35, 5, 623, DateTimeKind.Local).AddTicks(7756) });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_IDAccount",
                table: "Rates",
                column: "IDAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_IDProduct",
                table: "Rates",
                column: "IDProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "abc", 1L, "admin" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 16, 23, 44, 55, 33, DateTimeKind.Local).AddTicks(5741), new DateTime(2022, 11, 16, 23, 44, 55, 33, DateTimeKind.Local).AddTicks(5732) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumns: new[] { "Email", "ID", "Username" },
                keyValues: new object[] { "zxxz", 2L, "user" },
                columns: new[] { "BirthDay", "CreatedDate" },
                values: new object[] { new DateTime(2022, 11, 16, 23, 44, 55, 33, DateTimeKind.Local).AddTicks(5745), new DateTime(2022, 11, 16, 23, 44, 55, 33, DateTimeKind.Local).AddTicks(5744) });
        }
    }
}
