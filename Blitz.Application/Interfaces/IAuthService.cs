using Blitz.Application.Dtos;

namespace Blitz.Application.Interfaces
{
    public interface IAuthService
    {
        Task<BlitzWrapper<SingleUser>> GetUserByEmail(string email);
        Task<BlitzWrapper<SingleUser>> GetUserById(string id);
        Task<BlitzWrapper<UserDetailsModel>> Login(UserLoginModel userLoginModel);
        Task<BlitzWrapper<UserDetailsModel>> Signup(UserRegisterModel userRegisterModel);
    }
}
