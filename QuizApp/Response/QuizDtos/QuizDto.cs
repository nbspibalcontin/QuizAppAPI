namespace QuizApp.Response.QuizDtos
{
    public class QuizDto
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
