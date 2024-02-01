using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizApp.Entity;

namespace QuizApp.Data
{
    public class QuizAppApiDbContext : IdentityDbContext<User>
    {
        public QuizAppApiDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne(question => question.Quiz)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(answer => answer.Question)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<QuizAnswer>()
                .HasOne(answer => answer.Quiz)
                .WithMany(quiz => quiz.QuizAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<User>()
                .HasOne(user => user.Score)
                .WithOne(score => score.User)
                .HasForeignKey<QuizScore>(score => score.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizScore> Scores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
    }

}
