using Microsoft.AspNetCore.Identity;

namespace QuizApp.Entity
{
    public class User : IdentityUser
    {
        public string? Fullname { get; set; }
        public int? Age { get; set; }
        public virtual ICollection<QuizAnswer> Answers { get; set; }
        public virtual QuizScore Score { get; set; }
    }
}
