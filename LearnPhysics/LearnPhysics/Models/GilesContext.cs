using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LearnPhysics.Models
{
    public partial class GilesContext : DbContext
    {
        public GilesContext()
        {
        }

        public GilesContext(DbContextOptions<GilesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestion { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLesson> UserLesson { get; set; }
        public virtual DbSet<UserQuiz> UserQuiz { get; set; }
        public virtual DbSet<UserQuizQuestion> UserQuizQuestion { get; set; }

        // Database connection string kep here.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk;Initial Catalog=Giles;User ID=Giles;Password=10147671;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        public void Reset()
        {
            var entries = this.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToArray();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                    entry.State = EntityState.Unchanged;
                else if (entry.State == EntityState.Added)
                    entry.State = EntityState.Detached;
                else if (entry.State == EntityState.Deleted)
                    entry.Reload();
            }
        }

        // Code below outlines the table structure in the database.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.LessonId).HasColumnName("Lesson_Id");

                entity.Property(e => e.LessonName)
                    .IsRequired()
                    .HasColumnName("Lesson_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TopicId).HasColumnName("Topic_Id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_ToTopic");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.Property(e => e.QuizName)
                    .IsRequired()
                    .HasColumnName("Quiz_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TopicId).HasColumnName("Topic_Id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quiz_ToTopic");
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.Property(e => e.QuizQuestionId).HasColumnName("Quiz_Question_Id");

                entity.Property(e => e.QuestionNumber).HasColumnName("Question_Number");

                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizQuestion)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizQuestion_ToQuiz");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId).HasColumnName("Topic_Id");

                entity.Property(e => e.TopicTitle)
                    .IsRequired()
                    .HasColumnName("Topic_Title")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<UserLesson>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.LessonId).HasColumnName("Lesson_Id");

                entity.HasKey(o => new { o.UserId, o.LessonId });
            });

            modelBuilder.Entity<UserQuiz>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.QuizId).HasColumnName("Quiz_Id");

                entity.HasKey(o => new { o.UserId, o.QuizId });

                entity.Property(e => e.CorrectAnswers).HasColumnName("Correct_Answers");

                entity.Property(e => e.TotalAnswers).HasColumnName("Total_Answers");

                entity.Property(e => e.Percentage).HasColumnName("Percentage");
            });

            modelBuilder.Entity<UserQuizQuestion>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.QuizQuestionId).HasColumnName("Quiz_Question_Id");

                entity.HasKey(o => new { o.UserId, o.QuizQuestionId });

                entity.Property(e => e.Correct).HasColumnName("Correct");

                entity.Property(e => e.AnswerText)
                    .IsRequired()
                    .HasColumnName("Answer_Text")
                    .IsUnicode(false);
            });
        }
    }
}
