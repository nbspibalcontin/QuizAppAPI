using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Entity;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.User;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepository;
        private readonly UserManager<User> _userManager;

        public UserController(IUser userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        //Add User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
        {
            try
            {

                // Validation
                var validationContext = new ValidationContext(userRequest, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(userRequest, validationContext, validationResults, validateAllProperties: true))
                {
                    // Validation failed
                    string errorMessage = string.Join(", ", validationResults.Select(result => result.ErrorMessage));
                    throw new ArgumentException($"Validation failed: {errorMessage}");
                }

                var result = await _userRepository.CreateUser(userRequest);

                return Ok(result);
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
