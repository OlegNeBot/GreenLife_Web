using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

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

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<CheckList> CheckList { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<DayPhrase> DayPhrase { get; set; }
        public virtual DbSet<Element> Element { get; set; }
        public virtual DbSet<Habit> Habit { get; set; }
        public virtual DbSet<HabitPerformance> HabitPerformance { get; set; }
        public virtual DbSet<HabitPhrase> HabitPhrase { get; set; }
        public virtual DbSet<Type> HabitType { get; set; }
        public virtual DbSet<Memo> Memo { get; set; }
        public virtual DbSet<Planet> Planet { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<StartPage> StartPage { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAnswer> UserAnswer { get; set; }

        #endregion

        #region [Config]

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var builder = new ConfigurationBuilder();
                /*builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");*/
                //var config = builder.Build();
                //string conString = config.GetConnectionString("DefaultConnection");

                //optionsBuilder.UseNpgsql(conString);
                //optionsBuilder.UseNpgsql("Host=45.10.244.15;Port=55532;Database=work100005;Username=work100005;Password=– ce*aT6PR27jN~_$bCM}s");
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=greenlife_db;Username=postgres;Password=oleggol12");
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

                entity.Property(e => e.Login)
                .IsRequired()
                .HasColumnName("login");

                entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(64)")
                .HasColumnName("password");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");

                entity.Property(e => e.FamilyName)
                .IsRequired()
                .HasColumnName("familyname");

                entity.Property(e => e.UserSex)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("sex");

                entity.Property(e => e.DateOfBirth)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("date_of_birth");

                entity.Property(e => e.RegDate)
                .IsRequired()
                .HasColumnType("timestamptz")
                .HasColumnName("reg_date");

                entity.Property(e => e.ScoreSum)
                .IsRequired()
                .HasColumnName("score_sum");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("usr_acc_fk");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("answ_pk");

                entity.ToTable("answer");

                entity.Property(e => e.AnswerText)
                .IsRequired()
                .HasColumnName("answer_text");

                entity.HasOne(d => d.Question)
                .WithMany(p => p.Answer);
            });

            modelBuilder.Entity<CheckList>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("checklist_pk");

                entity.ToTable("checklist");

                entity.Property(e => e.ExecStatus)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("exec_status");

                entity.Property(e => e.CheckListName)
                .IsRequired()
                .HasColumnName("checklist_name");

                entity.HasOne(d => d.Type)
                .WithMany(p => p.CheckList);

                entity.HasMany(p => p.Habit)
                .WithMany(d => d.CheckList);

                entity.HasOne(p => p.User)
                .WithMany(d => d.CheckList);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("color_pk");

                entity.ToTable("color");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");

                entity.Property(e => e.Content)
                .IsRequired()
                .HasColumnName("content");
            });

            modelBuilder.Entity<DayPhrase>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("day_phrase_pk");

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

                entity.Property(e => e.Score)
                .IsRequired()
                .HasColumnName("score");

                entity.Property(e => e.HabitName)
                .IsRequired()
                .HasColumnName("habit_name");

                entity.Property(e => e.NumsNeeded)
                .IsRequired()
                .HasColumnName("nums_needed");

                entity.Property(e => e.ExecProperty)
                .IsRequired()
                .HasColumnName("exec_property");

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

                entity.Property(e => e.NumOfExecs)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("num_of_execs");

                entity.Property(e => e.DateOfExec)
                .IsRequired()
                .HasColumnType("timestamptz")
                .HasColumnName("date_of_exec");

                entity.Property(e => e.Executed)
                .IsRequired()
                .HasColumnName("executed")
                .HasDefaultValue(false);

                entity.HasOne(p => p.User)
                .WithMany(d => d.HabitPerformance);

                entity.HasOne(p => p.Habit)
                .WithMany(d => d.HabitPerformance);
            });

            modelBuilder.Entity<HabitPhrase>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("habit_phrase_pk");

                entity.ToTable("habit_phrase");

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

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("hab_type_pk");

                entity.ToTable("habit_type");

                entity.Property(e => e.NameType)
                .IsRequired()
                .HasColumnName("name_type");
            });

            modelBuilder.Entity<Memo>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("memo_pk");

                entity.ToTable("memo");

                entity.Property(e => e.MemoName)
                .IsRequired()
                .HasColumnName("memo_name");

                entity.Property(e => e.MemoRef)
                .IsRequired()
                .HasColumnName("memo_ref");
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("plan_pk");

                entity.ToTable("planet");

                entity.Property(e => e.PlanetRef)
                .IsRequired()
                .HasColumnName("planet_ref");

                entity.HasOne(d => d.StartPage)
                .WithOne(p => p.Planet)
                .HasForeignKey<Planet>(d => d.StartPageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("start_plan_fk");

                entity.HasMany(p => p.Color)
                .WithMany(d => d.Planet);

                entity.HasMany(p => p.Element)
                .WithMany(d => d.Planet);
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("elem_pk");

                entity.ToTable("elem");

                entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");

                entity.Property(e => e.Content)
                .IsRequired()
                .HasColumnName("content");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("quest_pk");

                entity.ToTable("question");

                entity.Property(e => e.QuestText)
                .IsRequired()
                .HasColumnName("quest_text");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("role_pk");

                entity.ToTable("role");

                entity.Property(e => e.UserRole)
                .IsRequired()
                .HasColumnName("user_role");
            });

            modelBuilder.Entity<StartPage>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("start_pk");

                entity.ToTable("start_page");

                entity.HasOne(d => d.User)
                .WithOne(p => p.StartPage)
                .HasForeignKey<StartPage>(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("us_start_fk");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("usr_pk");

                entity.ToTable("usr");

                entity.HasOne(d => d.Role)
                .WithMany(p => p.User);
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("us_an_pkey");

                entity.ToTable("user_answer");

                entity.HasOne(d => d.Answer)
                .WithMany(p => p.UserAnswer);

                entity.HasOne(d => d.Question)
                .WithMany(p => p.UserAnswer);

                entity.HasOne(d => d.Account)
                .WithMany(p => p.UserAnswer);
            });

        }

        #endregion

    }
}

