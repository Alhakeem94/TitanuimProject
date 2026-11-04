using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TitaniumBackendApi.Migrations
{
    /// <inheritdoc />
    public partial class addingAccountTableToDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsTable_Customers_CustomerId",
                table: "AccountsTable");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsTable_Customers_CustomerId",
                table: "AccountsTable",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsTable_Customers_CustomerId",
                table: "AccountsTable");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsTable_Customers_CustomerId",
                table: "AccountsTable",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
