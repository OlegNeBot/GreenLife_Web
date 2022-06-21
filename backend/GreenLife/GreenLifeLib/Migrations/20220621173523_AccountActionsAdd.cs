using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GreenLifeLib.Migrations
{
    public partial class AccountActionsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_actions_account_AccountId",
                table: "account_actions");

            migrationBuilder.DropForeignKey(
                name: "FK_account_actions_action_ActionId",
                table: "account_actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_account_actions",
                table: "account_actions");

            migrationBuilder.RenameTable(
                name: "account_actions",
                newName: "account_action");

            migrationBuilder.RenameIndex(
                name: "IX_account_actions_ActionId",
                table: "account_action",
                newName: "IX_account_action_ActionId");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "account_action",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "account_action_pk",
                table: "account_action",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_account_action_AccountId",
                table: "account_action",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_account_action_account_AccountId",
                table: "account_action",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_account_action_action_ActionId",
                table: "account_action",
                column: "ActionId",
                principalTable: "action",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_action_account_AccountId",
                table: "account_action");

            migrationBuilder.DropForeignKey(
                name: "FK_account_action_action_ActionId",
                table: "account_action");

            migrationBuilder.DropPrimaryKey(
                name: "account_action_pk",
                table: "account_action");

            migrationBuilder.DropIndex(
                name: "IX_account_action_AccountId",
                table: "account_action");

            migrationBuilder.DropColumn(
                name: "id",
                table: "account_action");

            migrationBuilder.RenameTable(
                name: "account_action",
                newName: "account_actions");

            migrationBuilder.RenameIndex(
                name: "IX_account_action_ActionId",
                table: "account_actions",
                newName: "IX_account_actions_ActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_account_actions",
                table: "account_actions",
                columns: new[] { "AccountId", "ActionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_account_actions_account_AccountId",
                table: "account_actions",
                column: "AccountId",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_account_actions_action_ActionId",
                table: "account_actions",
                column: "ActionId",
                principalTable: "action",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
