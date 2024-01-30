namespace QuizApp.Response.QuizScore
{
    public class QuestionScoreDto
    {
        public int QuestionId { get; set; }
        public int CorrectOptionIndex { get; set; }
        public int SelectedOptionIndex { get; set; }
        public bool IsCorrect { get; set; }
    }
}
