using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TRANSACTIONS",
                columns: table => new
                {
                    TRANSACTION_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AMOUNT = table.Column<decimal>(type: "numeric", nullable: false),
                    CURRENCY = table.Column<int>(type: "integer", nullable: false),
                    TRANSACTION_DATE = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FROM_ACCOUNT_ID = table.Column<long>(type: "bigint", nullable: false),
                    TO_ACCOUNT_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACTIONS", x => x.TRANSACTION_ID);
                    table.ForeignKey(
                        name: "FK_TRANSACTIONS_ACCOUNTS_FROM_ACCOUNT_ID",
                        column: x => x.FROM_ACCOUNT_ID,
                        principalTable: "ACCOUNTS",
                        principalColumn: "ACCOUNT_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TRANSACTIONS_ACCOUNTS_TO_ACCOUNT_ID",
                        column: x => x.TO_ACCOUNT_ID,
                        principalTable: "ACCOUNTS",
                        principalColumn: "ACCOUNT_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_FROM_ACCOUNT_ID",
                table: "TRANSACTIONS",
                column: "FROM_ACCOUNT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTIONS_TO_ACCOUNT_ID",
                table: "TRANSACTIONS",
                column: "TO_ACCOUNT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRANSACTIONS");
        }
    }
}
