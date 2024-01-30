namespace QuizApp.Response.QuizAnswerDtos
{
    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int SelectedOptionIndex { get; set; }
    }
}
