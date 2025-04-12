using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTS",
                columns: table => new
                {
                    CLIENT_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LOGIN = table.Column<string>(type: "text", nullable: false),
                    PASSWORD = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTS", x => x.CLIENT_ID);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNTS",
                columns: table => new
                {
                    ACCOUNT_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BALANCE = table.Column<decimal>(type: "numeric", nullable: false),
                    CURRENCY = table.Column<string>(type: "text", nullable: false),
                    CLIENT_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNTS", x => x.ACCOUNT_ID);
                    table.ForeignKey(
                        name: "FK_ACCOUNTS_CLIENTS_CLIENT_ID",
                        column: x => x.CLIENT_ID,
                        principalTable: "CLIENTS",
                        principalColumn: "CLIENT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNTS_CLIENT_ID",
                table: "ACCOUNTS",
                column: "CLIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Login",
                table: "CLIENTS",
                column: "LOGIN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNTS");

            migrationBuilder.DropTable(
                name: "CLIENTS");
        }
    }
}
