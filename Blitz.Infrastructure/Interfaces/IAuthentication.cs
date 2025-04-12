using Blitz.Domain.Entities;

namespace Blitz.Infrastructure.Interfaces
{
    public interface IAuthentication
    {
        Task<User> Login(string email, string password);
        Task<User> Signup(string username, string password, string email);
        Task<User> GetUserById(string userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
    }
}