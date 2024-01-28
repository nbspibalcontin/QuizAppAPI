namespace QuizApp.Response
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}
