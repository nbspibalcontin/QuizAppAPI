namespace QuizApp.Entity
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
        public int QuizId { get; set; } // Foreign key 
        public virtual Quiz Quiz { get; set; }
    }
}
