using System.ComponentModel.DataAnnotations;

namespace QuizApp.Request
{
    public class QuestionRequest
    {
        [Required(ErrorMessage = "Question text is required.")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Options are required.")]
        [MinLength(2, ErrorMessage = "At least two options are required.")]
        public List<string> Options { get; set; }

        [Required(ErrorMessage = "Correct option index is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Correct option index must be a non-negative integer.")]
        public int CorrectOptionIndex { get; set; }
    }
}
