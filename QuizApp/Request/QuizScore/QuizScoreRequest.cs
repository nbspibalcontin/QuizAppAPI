namespace QuizApp.Request.QuizScore
{
    public class QuizScoreRequest
    {
        public int QuizId { get; set; }
        public int Score { get; set; }
        public DateTime QuizTakenDateTime { get; set; }
        public TimeSpan QuizDuration { get; set; }
    }
}
