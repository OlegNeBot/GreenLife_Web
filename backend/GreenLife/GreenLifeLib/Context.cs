using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GreenLifeLib
{
    public class Context : DbContext
    {
        #region [Constructors]

        public Context() { }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        #endregion

        #region [DbSets]

        public DbSet<Account> Account { get; set; } = null!;
        public DbSet<AccountAction> AccountAction { get; set; } = null!;
        public DbSet<Action> Action { get; set; } = null!;
        public DbSet<ActionType> ActionType { get; set; } = null!;
        public DbSet<Answer> Answer { get; set; } = null!;
        public DbSet<CheckList> CheckList { get; set; } = null!;
        public DbSet<CheckListName> CheckListName { get; set; } = null!;
        public DbSet<DayPhrase> DayPhrase { get; set; } = null!;
        public DbSet<Habit> Habit { get; set; } = null!;
        public DbSet<HabitPerformance> HabitPerformance { get; set; } = null!;
        public DbSet<HabitPhrase> HabitPhrase { get; set; } = null!;
        public DbSet<Memo> Memo { get; set; } = null!;
        public DbSet<Question> Question { get; set; } = null!;
        public DbSet<Role> Role { get; set; } = null!;
        public DbSet<Token> Token { get; set; } = null!;
        public DbSet<Type> Type { get; set; } = null!;

        #endregion

        #region [Config]

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                string conString = config.GetConnectionString("LocalConnection");
                //string conString = config.GetConnectionString("RemoteConnection");

                optionsBuilder.UseNpgsql(conString);
            }
        }

        #endregion

        #region [DbCreating]

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("account_pk");

                entity.ToTable("account");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(64)")
                .HasColumnName("password");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");

                entity.Property(e => e.RegDate)
                .IsRequired()
                .HasColumnName("reg_date");

                entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("text");

                entity.Property(e => e.ScoreSum)
                .IsRequired()
                .HasColumnName("score_sum");

                entity.HasOne(d => d.Role)
                .WithMany(p => p.Account);

                entity.HasOne(p => p.Token)
                .WithOne(d => d.Account)
                .HasForeignKey<Token>(p => p.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("token_account_fk");
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("action_pk");

                entity.ToTable("action");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.ActionName)
                .IsRequired()
                .HasColumnName("action_name");

                entity.HasOne(d => d.ActionType)
                .WithMany(p => p.Action);

            });

            modelBuilder.Entity<AccountAction>(entity => 
            {
                entity.HasKey(e => e.Id)
                .HasName("account_action_pk");

                entity.ToTable("account_action");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.ActionDate)
                .IsRequired()
                .HasColumnName("action_date");

                entity.HasOne(p => p.Action)
                .WithMany(d => d.AccountAction);

                entity.HasOne(p => p.Account)
                .WithMany(d => d.AccountAction);
            });

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.HasKey(p => p.Id)
                    .HasName("action_type_id");

                entity.ToTable("action_type");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(p => p.TypeName)
                    .IsRequired()
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("answer_pk");

                entity.ToTable("answer");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.AnswerText)
                .IsRequired()
                .HasColumnName("answer_text");

                entity.HasOne(d => d.Question)
                .WithMany(p => p.Answers);
            });

            modelBuilder.Entity<CheckList>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("checklist_pk");

                entity.ToTable("checklist");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.ExecutionStatus)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("execution_status");

                entity.HasOne(d => d.Type)
                .WithMany(p => p.CheckList);

                entity.HasMany(p => p.Habit)
                .WithMany(d => d.CheckList);

                entity.HasOne(p => p.Account)
                .WithMany(d => d.CheckList);

                entity.HasOne(p => p.CheckListName)
                .WithMany(d => d.CheckList);
            });

            modelBuilder.Entity<CheckListName>(entity => 
            {
                entity.HasKey(e => e.Id)
                .HasName("checklist_name_pk");

                entity.ToTable("checklist_name");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");
            });

            modelBuilder.Entity<DayPhrase>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("day_phrase_pk");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.ToTable("day_phrase");

                entity.Property(e => e.PhraseText)
                .IsRequired()
                .HasColumnName("phrase_text");
            });

            modelBuilder.Entity<Habit>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("habit_pk");

                entity.ToTable("habit");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.Score)
                .IsRequired()
                .HasColumnName("score");

                entity.Property(e => e.HabitName)
                .IsRequired()
                .HasColumnName("habit_name");

                entity.Property(e => e.Total)
                .IsRequired()
                .HasColumnName("total");

                entity.HasOne(d => d.HabitPhrase)
                .WithOne(p => p.Habit)
                .HasForeignKey<HabitPhrase>(p => p.HabitId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("hab_habph_fk");

                entity.HasOne(d => d.Type)
                .WithMany(p => p.Habit);

            });

            modelBuilder.Entity<HabitPerformance>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("hab_perf_pk");

                entity.ToTable("habit_performance");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.NumOfExecs)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("num_of_execs");

                entity.Property(e => e.DateOfExec)
                .HasColumnType("timestamptz")
                .HasColumnName("date_of_exec");

                entity.Property(e => e.Executed)
                .IsRequired()
                .HasColumnName("executed")
                .HasDefaultValue(false);

                entity.HasOne(p => p.Account)
                .WithMany(d => d.HabitPerformance);

                entity.HasOne(p => p.Habit)
                .WithMany(d => d.HabitPerformance);
            });

            modelBuilder.Entity<HabitPhrase>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("habit_phrase_pk");

                entity.ToTable("habit_phrase");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.PhraseText)
                .IsRequired()
                .HasColumnName("phrase_text");

                entity.HasOne(p => p.Habit)
                .WithOne(d => d.HabitPhrase)
                .HasForeignKey<HabitPhrase>(d => d.HabitId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("haph_ha_fk");
                ;
            });

            modelBuilder.Entity<Memo>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("memo_pk");

                entity.ToTable("memo");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.MemoName)
                .IsRequired()
                .HasColumnName("memo_name");

                entity.Property(e => e.MemoRef)
                .IsRequired()
                .HasColumnName("memo_ref");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("quest_pk");

                entity.ToTable("question");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.QuestionText)
                .IsRequired()
                .HasColumnName("question_text");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("role_pk");

                entity.ToTable("role");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.RoleName)
                .IsRequired()
                .HasColumnName("role_name");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("token_pk");

                entity.ToTable("token");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.UserToken)
                .IsRequired()
                .HasColumnName("user_token");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("type_pk");

                entity.ToTable("type");

                entity.Property(e => e.Id)
                .HasColumnName("id");

                entity.Property(e => e.TypeName)
                .IsRequired()
                .HasColumnName("type_name");
            });
        }

        #endregion

    }
}
