namespace QuizApp.Entity
{
    public class QuizAnswer
    {
        public int QuizAnswerId { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int SelectedOptionIndex { get; set; }
        public DateTime AnswerDateTime { get; set; }
    }
}
