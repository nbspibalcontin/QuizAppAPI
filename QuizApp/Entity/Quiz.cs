namespace QuizApp.Entity
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
