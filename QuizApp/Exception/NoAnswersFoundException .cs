namespace QuizApp.Exception
{
    public class NoAnswersFoundException : System.Exception
    {
        public NoAnswersFoundException()
        {
        }

        public NoAnswersFoundException(string message)
            : base(message)
        {
        }

        public NoAnswersFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
