using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GreenLifeLib.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "action_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("action_type_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "checklist_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("checklist_name_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "day_phrase",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phrase_text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("day_phrase_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "memo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    memo_name = table.Column<string>(type: "text", nullable: false),
                    memo_ref = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("memo_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question_text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("quest_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "action",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action_name = table.Column<string>(type: "text", nullable: false),
                    ActionTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("action_pk", x => x.id);
                    table.ForeignKey(
                        name: "FK_action_action_type_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "action_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    answer_text = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("answer_pk", x => x.id);
                    table.ForeignKey(
                        name: "FK_answer_question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    password = table.Column<string>(type: "varchar(64)", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    reg_date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    score_sum = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("account_pk", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "habit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    score = table.Column<int>(type: "integer", nullable: false),
                    habit_name = table.Column<string>(type: "text", nullable: false),
                    total = table.Column<int>(type: "integer", nullable: false),
                    HabitPhraseId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("habit_pk", x => x.id);
                    table.ForeignKey(
                        name: "FK_habit_type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_actions",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    action_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_actions", x => new { x.AccountId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_account_actions_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_actions_action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checklist",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    execution_status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    CheckListNameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("checklist_pk", x => x.id);
                    table.ForeignKey(
                        name: "FK_checklist_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checklist_checklist_name_CheckListNameId",
                        column: x => x.CheckListNameId,
                        principalTable: "checklist_name",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checklist_type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "habit_performance",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    num_of_execs = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    date_of_exec = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    executed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    HabitId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hab_perf_pk", x => x.id);
                    table.ForeignKey(
                        name: "FK_habit_performance_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_habit_performance_habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "habit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "habit_phrase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phrase_text = table.Column<string>(type: "text", nullable: false),
                    HabitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("habit_phrase_pk", x => x.Id);
                    table.ForeignKey(
                        name: "haph_ha_fk",
                        column: x => x.HabitId,
                        principalTable: "habit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckListHabit",
                columns: table => new
                {
                    CheckListId = table.Column<int>(type: "integer", nullable: false),
                    HabitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListHabit", x => new { x.CheckListId, x.HabitId });
                    table.ForeignKey(
                        name: "FK_CheckListHabit_checklist_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "checklist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckListHabit_habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "habit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_RoleId",
                table: "account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_account_actions_ActionId",
                table: "account_actions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_action_ActionTypeId",
                table: "action",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_answer_QuestionId",
                table: "answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_checklist_AccountId",
                table: "checklist",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_checklist_CheckListNameId",
                table: "checklist",
                column: "CheckListNameId");

            migrationBuilder.CreateIndex(
                name: "IX_checklist_TypeId",
                table: "checklist",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListHabit_HabitId",
                table: "CheckListHabit",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_habit_TypeId",
                table: "habit",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_habit_performance_AccountId",
                table: "habit_performance",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_habit_performance_HabitId",
                table: "habit_performance",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_habit_phrase_HabitId",
                table: "habit_phrase",
                column: "HabitId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_actions");

            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "CheckListHabit");

            migrationBuilder.DropTable(
                name: "day_phrase");

            migrationBuilder.DropTable(
                name: "habit_performance");

            migrationBuilder.DropTable(
                name: "habit_phrase");

            migrationBuilder.DropTable(
                name: "memo");

            migrationBuilder.DropTable(
                name: "action");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "checklist");

            migrationBuilder.DropTable(
                name: "habit");

            migrationBuilder.DropTable(
                name: "action_type");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "checklist_name");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
