using System.ComponentModel.DataAnnotations;

namespace QuizApp.Request.Answer
{
    public class SubmitQuizAnswersRequest
    {
        [Required(ErrorMessage = "QuizId is required.")]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "QuestionAnswers is required.")]
        [MinLength(1, ErrorMessage = "At least one question answer must be provided.")]
        public List<QuestionAnswerDto> QuestionAnswers { get; set; }
    }
}
