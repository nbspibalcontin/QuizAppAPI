using Microsoft.AspNetCore.Mvc;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.Answer;

namespace QuizApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AnswerController : Controller
    {
        private readonly IAnswer _answerRepository;

        public AnswerController(IAnswer answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [HttpPost]
        public IActionResult CreateAnswer([FromBody] SubmitQuizAnswersRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid quiz request");
                }

                return Ok(_answerRepository.AnswerQuestion(request));
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
    }
}
