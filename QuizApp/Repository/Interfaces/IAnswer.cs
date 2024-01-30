using QuizApp.Request.Answer;
using QuizApp.Response;
using QuizApp.Response.QuizAnswerDtos;

namespace QuizApp.Repository.Interfaces
{
    public interface IAnswer : IDisposable
    {
        MessageResponse AnswerQuestion(SubmitQuizAnswersRequest request);
        List<QuizAnswerDto> GetAnswer(int QuizId);


    }
}
