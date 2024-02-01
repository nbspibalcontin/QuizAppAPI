namespace QuizApp.Entity
{
    public class QuizScore
    {
        public int QuizScoreId { get; set; }
        public int Score { get; set; }
        public double Percentage { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
