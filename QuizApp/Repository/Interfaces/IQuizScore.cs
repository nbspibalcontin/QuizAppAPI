using QuizApp.Response;
using QuizApp.Response.QuizScore;

namespace QuizApp.Repository.Interfaces
{
    public interface IQuizScore : IDisposable
    {
        //Calculate the Score
        MessageResponse CalculateTheScore(int QuizId, int UserId);
        //Get the Score
        QuizScoreDto QuizScore(int QuizId, int UserId);
        //Delete Score
        MessageResponse DeleteScore(int QuizScoreId);
    }
}
