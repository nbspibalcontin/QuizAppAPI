using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Exception;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.Quiz;
using QuizApp.Response.QuizDtos;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class QuizController : Controller
    {
        private readonly IQuiz _quizRepository;
        private readonly IMapper _mapper;

        public QuizController(IQuiz quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        //Create Quiz
        [HttpPost]
        public IActionResult CreateQuiz([FromBody] QuizRequest quizRequest)
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

                return Ok(_quizRepository.CreateQuiz(quizRequest));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", ErrorMessage = ex.Message });
            }
        }

        //GetAllQuizzesAndQuestions
        [HttpGet]
        public IActionResult FindAllQuizAndQuestions()
        {
            try
            {
                var quizzes = _quizRepository.GetAllQuizzesAndQuestions();

                var quizzesDtos = _mapper.Map<List<QuizDto>>(quizzes);

                return Ok(quizzesDtos);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", ErrorMessage = ex.Message });
            }
        }

        //GetQuizAndQuestions
        [HttpGet("{id}")]
        public IActionResult GetQuiz(int id)
        {
            try
            {
                var quiz = _quizRepository.GetQuiz(id);

                var quizDto = _mapper.Map<QuizDto>(quiz);

                return Ok(quizDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        //Update QuizAndQuestions
        [HttpPut("{id}")]
        public IActionResult UpdateQuizAndQuestions(int id, QuizRequest quizRequest)
        {
            try
            {
                var quiz = _quizRepository.UpdateQuiz(id, quizRequest);

                return Ok(quiz);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        //Delete QuizAndQuestions
        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(int id)
        {
            try
            {
                var quiz = _quizRepository.DeleteQuiz(id);

                return Ok(quiz);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

    }
}
