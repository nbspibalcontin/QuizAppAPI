using Microsoft.AspNetCore.Mvc;
using QuizApp.Exception;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.QuizScore;

namespace QuizApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class QuizScoreController : Controller
    {
        private readonly IQuizScore _quizScoreRepository;

        public QuizScoreController(IQuizScore quizScoreRepository)
        {
            _quizScoreRepository = quizScoreRepository;
        }

        //Calculate the Score
        [HttpPost("CalculateQuizScore")]
        public IActionResult CalculateQuizScore([FromBody] QuizScoreRequest request)
        {
            try
            {
                return Ok(_quizScoreRepository.CalculateTheScore(request.QuizId, request.UserId));
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

        //Get the Scores
        [HttpGet("{QuizId}/{UserId}")]
        public IActionResult GetTheQuizScore(int QuizId, string UserId)
        {
            try
            {
                return Ok(_quizScoreRepository.QuizScore(QuizId, UserId));
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

        //Delete Quiz Score
        [HttpDelete("{QuizScoreId}")]
        public IActionResult DeleteQuizScore(int QuizScoreId)
        {
            try
            {
                return Ok(_quizScoreRepository.DeleteScore(QuizScoreId));
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
    }
}
