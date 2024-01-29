using QuizApp.Request.Answer;
using QuizApp.Response;

namespace QuizApp.Repository.Interfaces
{
    public interface IAnswer : IDisposable
    {
        MessageResponse AnswerQuestion(SubmitQuizAnswersRequest request);
    }
}
