using QuizApp.Entity;
using QuizApp.Request;
using QuizApp.Response;

namespace QuizApp.Repository.Interfaces
{
    public interface IQuizRepository : IDisposable
    {
        //Create Quiz
        MessageResponse CreateQuiz(QuizRequest quizRequest);

        //GetAllQuizAndQuestion
        List<Quiz> GetAllQuizzesAndQuestions();

        //GetQuizAndQuestions
        Quiz GetQuiz(int id);

        //UpdateQuizAndQuestions
        MessageResponse UpdateQuiz(int id, QuizRequest quizRequest);

        //DeleteQuizAndQuestions
        MessageResponse DeleteQuiz(int id);
    }
}
