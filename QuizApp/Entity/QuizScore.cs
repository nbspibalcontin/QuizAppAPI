namespace QuizApp.Entity
{
    public class QuizScore
    {
        public int QuizScoreId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
        public DateTime QuizTakenDateTime { get; set; }
        public TimeSpan QuizDuration { get; set; }
    }
}
