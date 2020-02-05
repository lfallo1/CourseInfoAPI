using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;

namespace CourseInfoAPI.Services
{
    public interface IAuthenticationService
    {
        string GenerateJsonWebToken(User user);
        User AuthenticateUser(LoginDto user);
        User GetUser(string username);
    }
}
