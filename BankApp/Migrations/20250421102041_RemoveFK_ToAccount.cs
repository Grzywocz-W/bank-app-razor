using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFK_ToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_AccountId",
                table: "TRANSACTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_TO_ACCOUNT_ID",
                table: "TRANSACTIONS");

            migrationBuilder.DropIndex(
                name: "IX_TRANSACTIONS_AccountId",
                table: "TRANSACTIONS");

            migrationBuilder.DropIndex(
                name: "IX_TRANSACTIONS_TO_ACCOUNT_ID",
                table: "TRANSACTIONS");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TRANSACTIONS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "TRANSACTIONS",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_AccountId",
                table: "TRANSACTIONS",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_TO_ACCOUNT_ID",
                table: "TRANSACTIONS",
                column: "TO_ACCOUNT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_AccountId",
                table: "TRANSACTIONS",
                column: "AccountId",
                principalTable: "ACCOUNTS",
                principalColumn: "ACCOUNT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTIONS_ACCOUNTS_TO_ACCOUNT_ID",
                table: "TRANSACTIONS",
                column: "TO_ACCOUNT_ID",
                principalTable: "ACCOUNTS",
                principalColumn: "ACCOUNT_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
