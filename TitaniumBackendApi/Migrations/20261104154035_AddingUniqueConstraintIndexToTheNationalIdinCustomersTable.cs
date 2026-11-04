using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TitaniumBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingUniqueConstraintIndexToTheNationalIdinCustomersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomersTable",
                table: "CustomersTable");

            migrationBuilder.RenameTable(
                name: "CustomersTable",
                newName: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Customers",
                type: "nchar(12)",
                fixedLength: true,
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "UX_Customers_NationalId",
                table: "Customers",
                column: "NationalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "UX_Customers_NationalId",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "CustomersTable");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "CustomersTable",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(12)",
                oldFixedLength: true,
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomersTable",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomersTable",
                table: "CustomersTable",
                column: "Id");
        }
    }
}
