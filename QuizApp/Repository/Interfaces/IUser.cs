using QuizApp.Request.User;
using QuizApp.Response;

namespace QuizApp.Repository.Interfaces
{
    public interface IUser : IDisposable
    {
        Task<MessageResponse> CreateUser(UserRequest request);
        Task<MessageResponse> CreateRole(string roleName);
    }
}
