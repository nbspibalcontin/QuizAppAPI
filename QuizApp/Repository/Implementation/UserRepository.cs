using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Repository.Interfaces;
using QuizApp.Request.User;
using QuizApp.Response;

namespace QuizApp.Repository.Implementation
{
    public class UserRepository : IUser
    {
        private readonly QuizAppApiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserRepository(QuizAppApiDbContext dbContext, IMapper mapper, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        //Create User
        public async Task<MessageResponse> CreateUser(UserRequest request)
        {
            try
            {
                var user = _mapper.Map<User>(request);

                var result = await _userManager.CreateAsync(user, user.PasswordHash!);

                if (result.Succeeded)
                {
                    return new MessageResponse("User Created Successfully!");
                }
                else
                {
                    // Handle failed user creation
                    var errorMessages = string.Join(", ", result.Errors.Select(error => error.Description));
                    return new MessageResponse($"Failed to create user. Errors: {errorMessages}");
                }
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("Error: " + ex.Message);
            }
        }



        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
