using QuizApp.Request.Answer;
using QuizApp.Response;
using QuizApp.Response.QuizAnswerDtos;

namespace QuizApp.Repository.Interfaces
{
    public interface IAnswer : IDisposable
    {
        //Answer the Questions
        MessageResponse AnswerQuestion(SubmitQuizAnswersRequest request);
        //Get the Answers of the question
        List<QuizAnswerDto> GetAnswer(int QuizId);
        //Delete the answer of the user
        MessageResponse DeleteAnswerOfTheUser(int UserId);

    }
}
