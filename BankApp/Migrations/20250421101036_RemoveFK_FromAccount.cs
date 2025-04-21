using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFK_FromAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_FROM_ACCOUNT_ID",
                table: "TRANSACTIONS");

            migrationBuilder.DropIndex(
                name: "IX_TRANSACTIONS_FROM_ACCOUNT_ID",
                table: "TRANSACTIONS");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "TRANSACTIONS",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_AccountId",
                table: "TRANSACTIONS",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_AccountId",
                table: "TRANSACTIONS",
                column: "AccountId",
                principalTable: "ACCOUNTS",
                principalColumn: "ACCOUNT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_AccountId",
                table: "TRANSACTIONS");

            migrationBuilder.DropIndex(
                name: "IX_TRANSACTIONS_AccountId",
                table: "TRANSACTIONS");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TRANSACTIONS");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_FROM_ACCOUNT_ID",
                table: "TRANSACTIONS",
                column: "FROM_ACCOUNT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_FROM_ACCOUNT_ID",
                table: "TRANSACTIONS",
                column: "FROM_ACCOUNT_ID",
                principalTable: "ACCOUNTS",
                principalColumn: "ACCOUNT_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
