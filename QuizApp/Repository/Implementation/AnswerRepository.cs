using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Exception;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.Answer;
using QuizApp.Response;
using QuizApp.Response.QuizAnswerDtos;
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

        //Create Answer Question Function
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

                //Check if the QuizId is Exist
                var quizDoesNotExist = _dbContext.Quizzes.Any(q => q.QuizId == request.QuizId);

                if (!quizDoesNotExist)
                {
                    throw new NotFoundException($"No quiz found with ID: {request.QuizId}");
                }

                //Check if the User is Exist
                var userDoesNotExist = _dbContext.Users.Any(u => u.UserId == request.UserId);

                if (!userDoesNotExist)
                {
                    throw new NotFoundException($"No user found with ID: {request.UserId}");
                }

                // Check if all QuestionIds exist
                if (request.QuestionAnswers.Any(qa => !_dbContext.Questions.Select(q => q.QuestionId).Contains(qa.QuestionId)))
                {
                    return new MessageResponse($"No question found with ID: {string.Join(", ", request.QuestionAnswers.Select(qa => qa.QuestionId))}");
                }

                //Mapper
                var quizAnswers = _mapper.Map<List<QuizAnswer>>(request);
                quizAnswers.ForEach(qa => qa.QuizId = request.QuizId);

                // Set the AnswerDateTime property for each QuizAnswer
                DateTime currentDateTime = DateTime.Now;
                quizAnswers.ForEach(qa =>
                {
                    qa.QuizId = request.QuizId;
                    qa.AnswerDateTime = currentDateTime;
                });

                _dbContext.QuizAnswers.AddRange(quizAnswers);
                _dbContext.SaveChanges();

                return new MessageResponse("Answer submitted successfully.");
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                return new MessageResponse("Error : " + ex.Message);
            }
        }

        //Get Answers of Question by Id
        public List<QuizAnswerDto> GetAnswer(int quizId)
        {
            try
            {
                // Check if the QuizId exists
                var quizDoesNotExist = _dbContext.Quizzes.Any(q => q.QuizId == quizId);

                if (!quizDoesNotExist)
                {
                    throw new NotFoundException($"No quiz found with ID: {quizId}");
                }

                // Check if the quiz has answers
                if (!_dbContext.QuizAnswers.Any(qa => qa.QuizId == quizId))
                {
                    // Handle the case where the quiz has no answers
                    throw new NoAnswersFoundException($"No answers found for quiz with ID: {quizId}");
                }

                // Use AutoMapper to map QuizAnswer entities to QuizAnswerDto
                var quizAnswers = _dbContext.QuizAnswers
                    .Where(qa => qa.QuizId == quizId)
                    .Include(qa => qa.Question)
                    .GroupBy(qa => qa.UserId)
                    .Select(group => new QuizAnswerDto
                    {
                        UserId = group.Key,
                        QuizId = quizId,
                        QuestionAnswers = group.Select(qa => _mapper.Map<QuestionAnswerDto>(qa)).ToList()
                    })
                    .ToList();

                return quizAnswers;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (NoAnswersFoundException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }

        //Delete the Answer of User
        public MessageResponse DeleteAnswerOfTheUser(int userId)
        {
            try
            {
                // Check if the User exists
                var answersToDelete = _dbContext.QuizAnswers.Where(qa => qa.UserId == userId).ToList();

                if (answersToDelete.Any())
                {
                    _dbContext.QuizAnswers.RemoveRange(answersToDelete);
                    _dbContext.SaveChanges();
                    return new MessageResponse("Answers deleted successfully!");
                }
                else
                {
                    return new MessageResponse($"No answers found for the user with ID: {userId}");
                }
            }
            catch (NotFoundException)
            {
                throw; // If NotFoundException is already handled elsewhere
            }
            catch (System.Exception ex)
            {
                // Log the exception or handle it appropriately
                return new MessageResponse($"Error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
