using System.ComponentModel.DataAnnotations;

namespace QuizApp.Request.Quiz
{
    public class QuizRequest
    {
        [Required(ErrorMessage = "Quiz title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Questions are required.")]
        [MinLength(1, ErrorMessage = "At least one question is required.")]
        public List<QuestionRequest> Questions { get; set; }
    }
}
