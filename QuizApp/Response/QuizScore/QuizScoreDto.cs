

namespace QuizApp.Response.QuizScore
{
    public class QuizScoreDto
    {
        public int QuizScoreId { get; set; }
        public int QuizId { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
        public double Percentage { get; set; }
        public List<QuestionScoreDto> QuestionScores { get; set; }
    }
}
