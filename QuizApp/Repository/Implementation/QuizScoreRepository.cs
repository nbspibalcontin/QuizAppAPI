using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Exception;
using QuizApp.Repository.Interfaces;
using QuizApp.Response;
using QuizApp.Response.QuizScore;

namespace QuizApp.Repository.Implementation
{
    public class QuizScoreRepository : IQuizScore
    {
        public readonly QuizAppApiDbContext _dbContext;
        public readonly IMapper _mapper;
        public QuizScoreRepository(QuizAppApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //Calculate the Quiz Score
        public MessageResponse CalculateTheScore(int quizId, int userId)
        {
            try
            {
                var existingQuiz = _dbContext.Quizzes
                    .Include(q => q.Questions)
                    .FirstOrDefault(q => q.QuizId == quizId);

                if (existingQuiz == null)
                {
                    throw new NotFoundException("No quiz found with id: " + quizId);
                }

                // Check if the User is Exist
                var userDoesNotExist = _dbContext.Users.Any(u => u.UserId == userId);

                if (!userDoesNotExist)
                {
                    throw new NotFoundException($"No user found with ID: {userId}");
                }

                var quizAnswers = _dbContext.QuizAnswers
                    .Where(qa => qa.UserId == userId && qa.QuizId == quizId)
                    .ToList();

                // AutoMapper is configured to map Quiz to QuizScoreDto and QuizAnswer to QuestionScoreDto

                var quizScoreDto = new QuizScoreDto
                {
                    QuizId = existingQuiz.QuizId,
                    QuestionScores = existingQuiz.Questions.Select(q => new QuestionScoreDto
                    {
                        QuestionId = q.QuestionId,
                        CorrectOptionIndex = q.CorrectOptionIndex
                    }).ToList(),
                    UserId = userId
                };

                // Map QuizAnswers to QuestionScoreDto
                _mapper.Map<List<QuizAnswer>, List<QuestionScoreDto>>(quizAnswers, quizScoreDto.QuestionScores);

                // Calculate Score and Percentage
                int correctAnswersCount = quizScoreDto.QuestionScores.Count(qs => qs.IsCorrect);
                quizScoreDto.Score = correctAnswersCount;

                // Round off the Percentage property to two decimal places
                quizScoreDto.Percentage = Math.Round((double)correctAnswersCount / existingQuiz.Questions.Count * 100, 2);

                // Save QuizScore entity
                SaveQuizScore(quizScoreDto);

                return new MessageResponse("Quiz score calculated and saved successfully.");
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (SystemException ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }

        //Save Quiz Score function
        private void SaveQuizScore(QuizScoreDto quizScoreDto)
        {
            var quizScoreEntity = _mapper.Map<QuizScore>(quizScoreDto);

            _dbContext.Scores.Add(quizScoreEntity);
            _dbContext.SaveChanges();
        }

        //Get the Score of the Answer
        public QuizScoreDto QuizScore(int quizId, int userId)
        {
            try
            {
                var existingQuiz = _dbContext.Quizzes
               .Include(q => q.Questions)
               .FirstOrDefault(q => q.QuizId == quizId);

                if (existingQuiz == null)
                {
                    throw new NotFoundException("No quiz found with id: " + quizId);
                }

                // Check if the User is Exist
                var userDoesNotExist = _dbContext.Users.Any(u => u.UserId == userId);

                if (!userDoesNotExist)
                {
                    throw new NotFoundException($"No user found with ID: {userId}");
                }

                var quizAnswers = _dbContext.QuizAnswers
                    .Where(qa => qa.UserId == userId && qa.QuizId == quizId)
                    .ToList();

                // AutoMapper is configured to map Quiz to QuizScoreDto and QuizAnswer to QuestionScoreDto

                var quizScoreDto = new QuizScoreDto
                {
                    QuizId = existingQuiz.QuizId,
                    QuestionScores = existingQuiz.Questions.Select(q => new QuestionScoreDto
                    {
                        QuestionId = q.QuestionId,
                        CorrectOptionIndex = q.CorrectOptionIndex
                    }).ToList(),
                    UserId = userId
                };

                // Map QuizAnswers to QuestionScoreDto
                _mapper.Map<List<QuizAnswer>, List<QuestionScoreDto>>(quizAnswers, quizScoreDto.QuestionScores);

                // Calculate Score and Percentage
                int correctAnswersCount = quizScoreDto.QuestionScores.Count(qs => qs.IsCorrect);
                quizScoreDto.Score = correctAnswersCount;

                // Round off the Percentage property to two decimal places
                quizScoreDto.Percentage = Math.Round((double)correctAnswersCount / existingQuiz.Questions.Count * 100, 2);

                return quizScoreDto;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (SystemException ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }

        //Delete Quiz Score
        public MessageResponse DeleteScore(int QuizScoreId)
        {
            var quizScoreEntity = _dbContext.Scores.Find(QuizScoreId);

            if (quizScoreEntity == null)
            {
                throw new NotFoundException($"No quiz score found with ID: {QuizScoreId}");
            }

            _dbContext.Scores.Remove(quizScoreEntity);
            _dbContext.SaveChanges();

            return new MessageResponse("QuizScore Delete Successfully");
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
