namespace QuizApp.Exception
{
    public class NoQuizFoundException : ApplicationException
    {
        public NoQuizFoundException(string message) : base(message)
        {
        }
    }

}
