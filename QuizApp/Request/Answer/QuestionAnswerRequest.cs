using System.ComponentModel.DataAnnotations;

namespace QuizApp.Request.Answer
{
    public class QuestionAnswerRequest
    {
        [Required(ErrorMessage = "QuestionId is required.")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "SelectedOptionIndex is required.")]
        public int SelectedOptionIndex { get; set; }
    }
}
