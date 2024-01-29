using AutoMapper;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.Answer;
using QuizApp.Response;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Repository.Implementation
{
    public class AnswerRepository : IAnswer
    {
        private readonly QuizAppApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerRepository(IMapper mapper, QuizAppApiDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public MessageResponse AnswerQuestion(SubmitQuizAnswersRequest request)
        {
            try
            {
                // Validation
                var validationContext = new ValidationContext(request, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(request, validationContext, validationResults, validateAllProperties: true))
                {
                    // Validation failed
                    string errorMessage = string.Join(", ", validationResults.Select(result => result.ErrorMessage));
                    throw new ArgumentException($"Validation failed: {errorMessage}");
                }

                //Mapper
                var quizAnswers = _mapper.Map<List<QuizAnswer>>(request);
                quizAnswers.ForEach(qa => qa.QuizId = request.QuizId);

                _dbContext.QuizAnswers.AddRange(quizAnswers);
                _dbContext.SaveChanges();

                return new MessageResponse("Answer submitted successfully.");
            }
            catch (System.Exception ex)
            {
                return new MessageResponse("error " + ex.Message);
            }
        }


        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
