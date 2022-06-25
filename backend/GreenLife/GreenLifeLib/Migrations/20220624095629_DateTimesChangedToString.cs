using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenLifeLib.Migrations
{
    public partial class DateTimesChangedToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "action_date",
                table: "account_action",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "reg_date",
                table: "account",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "action_date",
                table: "account_action",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "reg_date",
                table: "account",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
