namespace QuizApp.Response.QuizAnswerDtos
{
    public class QuizAnswerDto
    {
        public string UserId { get; set; }
        public int QuizId { get; set; }
        public List<QuestionAnswerDto> QuestionAnswers { get; set; }
    }
}
