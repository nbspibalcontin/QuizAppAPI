using Microsoft.EntityFrameworkCore;
using QuizApp.Entity;

namespace QuizApp.Data
{
    public class QuizAppApiDbContext : DbContext
    {
        public QuizAppApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> questiones { get; set; }
        public DbSet<QuizScore> scores { get; set; }
        public DbSet<User> users { get; set; }
    }
}
