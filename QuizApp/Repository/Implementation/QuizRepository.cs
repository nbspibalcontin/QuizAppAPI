using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Exception;
using QuizApp.Repository.Interfaces;
using QuizApp.Request;
using QuizApp.Response;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Repository.Implementation
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizAppApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuizRepository(QuizAppApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //Create Quiz and Questions
        public MessageResponse CreateQuiz(QuizRequest quizRequest)
        {
            try
            {
                // Validation
                var validationContext = new ValidationContext(quizRequest, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(quizRequest, validationContext, validationResults, validateAllProperties: true))
                {
                    // Validation failed
                    string errorMessage = string.Join(", ", validationResults.Select(result => result.ErrorMessage));
                    throw new ArgumentException($"Validation failed: {errorMessage}");
                }

                // Mapping and database operation
                var quiz = _mapper.Map<Quiz>(quizRequest);
                _dbContext.Quizzes.Add(quiz);
                _dbContext.SaveChanges();

                return new MessageResponse("Quiz added successfully.");
            }
            catch (SystemException ex)
            {
                throw new ApplicationException("Error retrieving quizzes and questions.", ex);
            }
        }

        //GetAllQuizAndQuestions
        public List<Quiz> GetAllQuizzesAndQuestions()
        {
            try
            {
                var quizzes = _dbContext.Quizzes.Include(q => q.Questions).ToList();

                if (quizzes.Count == 0)
                {
                    throw new NotFoundException("No quizzes found.");
                }
                return quizzes;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("Error retrieving quizzes and questions.", ex);
            }
        }


        //GetQuizAndQuestions
        public Quiz GetQuiz(int id)
        {
            try
            {
                var existingQuiz = _dbContext.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.QuizId == id);

                if (existingQuiz == null)
                {
                    throw new NotFoundException("No quiz found with id : " + id);
                }

                return existingQuiz;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException($"Error retrieving quiz with id {id}.", ex);
            }
        }

        //Update Quiz and Questions
        public MessageResponse UpdateQuiz(int id, QuizRequest quizRequest)
        {
            try
            {
                //Check if the Quiz is exist
                var existingQuiz = _dbContext.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.QuizId == id);

                if (existingQuiz == null)
                {
                    throw new NotFoundException("No quiz found with id : " + id);
                }

                _mapper.Map(quizRequest, existingQuiz);

                //Update Question
                if (quizRequest.Questions != null && quizRequest.Questions.Any())
                {
                    existingQuiz.Questions = _mapper.Map<List<Question>>(quizRequest.Questions);
                }

                //Save Changes
                _dbContext.SaveChanges();

                return new MessageResponse("Quiz Updated Successfully");
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("Error retrieving quizzes and questions.", ex);
            }
        }

        public MessageResponse DeleteQuiz(int id)
        {
            try
            {
                // Check if the Quiz exists
                var existingQuiz = _dbContext.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.QuizId == id);

                if (existingQuiz == null)
                {
                    throw new NotFoundException("No quiz found with id : " + id);
                }

                _dbContext.Remove(existingQuiz);
                _dbContext.SaveChanges();

                return new MessageResponse("Quiz deleted successfully.");
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (System.Exception)
            {
                return new MessageResponse("Error deleting quiz. Please try again later.");
            }
        }


        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
