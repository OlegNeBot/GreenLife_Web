using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GreenLifeLib.Migrations
{
    public partial class TokenAccountChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "token_account_fk",
                table: "account");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "account",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_token_AccountId",
                table: "token",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "token_account_fk",
                table: "token",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "token_account_fk",
                table: "token");

            migrationBuilder.DropIndex(
                name: "IX_token_AccountId",
                table: "token");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "account",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "token_account_fk",
                table: "account",
                column: "id",
                principalTable: "token",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
