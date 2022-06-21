﻿// <auto-generated />
using System;
using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GreenLifeLib.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220618155519_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CheckListHabit", b =>
                {
                    b.Property<int>("CheckListId")
                        .HasColumnType("integer");

                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.HasKey("CheckListId", "HabitId");

                    b.HasIndex("HabitId");

                    b.ToTable("CheckListHabit");
                });

            modelBuilder.Entity("GreenLifeLib.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(64)")
                        .HasColumnName("password");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("timestamptz")
                        .HasColumnName("reg_date");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("ScoreSum")
                        .HasColumnType("integer")
                        .HasColumnName("score_sum");

                    b.HasKey("Id")
                        .HasName("account_pk");

                    b.HasIndex("RoleId");

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.AccountAction", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("ActionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("action_date");

                    b.HasKey("AccountId", "ActionId");

                    b.HasIndex("ActionId");

                    b.ToTable("account_actions", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("action_name");

                    b.Property<int>("ActionTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("action_pk");

                    b.HasIndex("ActionTypeId");

                    b.ToTable("action", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.ActionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id")
                        .HasName("action_type_id");

                    b.ToTable("action_type", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("answer_text");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("answer_pk");

                    b.HasIndex("QuestionId");

                    b.ToTable("answer", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("CheckListNameId")
                        .HasColumnType("integer");

                    b.Property<bool>("ExecutionStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("execution_status");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("checklist_pk");

                    b.HasIndex("AccountId");

                    b.HasIndex("CheckListNameId");

                    b.HasIndex("TypeId");

                    b.ToTable("checklist", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.CheckListName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("checklist_name_pk");

                    b.ToTable("checklist_name", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.DayPhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PhraseText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phrase_text");

                    b.HasKey("Id")
                        .HasName("day_phrase_pk");

                    b.ToTable("day_phrase", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HabitName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("habit_name");

                    b.Property<int>("HabitPhraseId")
                        .HasColumnType("integer");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<int>("Total")
                        .HasColumnType("integer")
                        .HasColumnName("total");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("habit_pk");

                    b.HasIndex("TypeId");

                    b.ToTable("habit", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.HabitPerformance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfExec")
                        .HasColumnType("timestamptz")
                        .HasColumnName("date_of_exec");

                    b.Property<bool>("Executed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("executed");

                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfExecs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("num_of_execs");

                    b.HasKey("Id")
                        .HasName("hab_perf_pk");

                    b.HasIndex("AccountId");

                    b.HasIndex("HabitId");

                    b.ToTable("habit_performance", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.HabitPhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.Property<string>("PhraseText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phrase_text");

                    b.HasKey("Id")
                        .HasName("habit_phrase_pk");

                    b.HasIndex("HabitId")
                        .IsUnique();

                    b.ToTable("habit_phrase", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Memo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MemoName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("memo_name");

                    b.Property<string>("MemoRef")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("memo_ref");

                    b.HasKey("Id")
                        .HasName("memo_pk");

                    b.ToTable("memo", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("question_text");

                    b.HasKey("Id")
                        .HasName("quest_pk");

                    b.ToTable("question", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_name");

                    b.HasKey("Id")
                        .HasName("role_pk");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("GreenLifeLib.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id")
                        .HasName("type_pk");

                    b.ToTable("type", (string)null);
                });

            modelBuilder.Entity("CheckListHabit", b =>
                {
                    b.HasOne("GreenLifeLib.CheckList", null)
                        .WithMany()
                        .HasForeignKey("CheckListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLifeLib.Habit", null)
                        .WithMany()
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenLifeLib.Account", b =>
                {
                    b.HasOne("GreenLifeLib.Role", "Role")
                        .WithMany("Account")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GreenLifeLib.AccountAction", b =>
                {
                    b.HasOne("GreenLifeLib.Account", "Account")
                        .WithMany("AccountAction")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLifeLib.Action", "Action")
                        .WithMany("AccountAction")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Action");
                });

            modelBuilder.Entity("GreenLifeLib.Action", b =>
                {
                    b.HasOne("GreenLifeLib.ActionType", "ActionType")
                        .WithMany("Action")
                        .HasForeignKey("ActionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActionType");
                });

            modelBuilder.Entity("GreenLifeLib.Answer", b =>
                {
                    b.HasOne("GreenLifeLib.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("GreenLifeLib.CheckList", b =>
                {
                    b.HasOne("GreenLifeLib.Account", "Account")
                        .WithMany("CheckList")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLifeLib.CheckListName", "CheckListName")
                        .WithMany("CheckList")
                        .HasForeignKey("CheckListNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLifeLib.Type", "Type")
                        .WithMany("CheckList")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("CheckListName");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("GreenLifeLib.Habit", b =>
                {
                    b.HasOne("GreenLifeLib.Type", "Type")
                        .WithMany("Habit")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("GreenLifeLib.HabitPerformance", b =>
                {
                    b.HasOne("GreenLifeLib.Account", "Account")
                        .WithMany("HabitPerformance")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenLifeLib.Habit", "Habit")
                        .WithMany("HabitPerformance")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("GreenLifeLib.HabitPhrase", b =>
                {
                    b.HasOne("GreenLifeLib.Habit", "Habit")
                        .WithOne("HabitPhrase")
                        .HasForeignKey("GreenLifeLib.HabitPhrase", "HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("haph_ha_fk");

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("GreenLifeLib.Account", b =>
                {
                    b.Navigation("AccountAction");

                    b.Navigation("CheckList");

                    b.Navigation("HabitPerformance");
                });

            modelBuilder.Entity("GreenLifeLib.Action", b =>
                {
                    b.Navigation("AccountAction");
                });

            modelBuilder.Entity("GreenLifeLib.ActionType", b =>
                {
                    b.Navigation("Action");
                });

            modelBuilder.Entity("GreenLifeLib.CheckListName", b =>
                {
                    b.Navigation("CheckList");
                });

            modelBuilder.Entity("GreenLifeLib.Habit", b =>
                {
                    b.Navigation("HabitPerformance");

                    b.Navigation("HabitPhrase")
                        .IsRequired();
                });

            modelBuilder.Entity("GreenLifeLib.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("GreenLifeLib.Role", b =>
                {
                    b.Navigation("Account");
                });

            modelBuilder.Entity("GreenLifeLib.Type", b =>
                {
                    b.Navigation("CheckList");

                    b.Navigation("Habit");
                });
#pragma warning restore 612, 618
        }
    }
}
