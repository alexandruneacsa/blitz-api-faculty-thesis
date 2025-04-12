using Blitz.Application.Dtos;
using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Blitz.Application.Mappers;
using Blitz.Domain.Entities;
using Blitz.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Blitz.Application.Services
{
    public class UserService : IAuthService
    {
        private readonly IAuthentication _authentication;
        private readonly IConfiguration _config;

        public UserService(IAuthentication authentication, IConfiguration configuration)
        {
            _authentication = authentication;
            _config = configuration;
        }

        public async Task<BlitzWrapper<SingleUser>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _authentication.GetUserByEmail(email);

                if (user == null)
                    return new BlitzWrapper<SingleUser>("Error", null, 404);

                var userMap = BlitzMapper.MapSourceToDestination(user);

                return new BlitzWrapper<SingleUser>("Result", userMap, 200);
            }
            catch (UnauthorizedAccessException ex)
            {
                return new BlitzWrapper<SingleUser>("Error" + ex.Message, null, 404);
            }
        }

        public async Task<BlitzWrapper<SingleUser>> GetUserById(string id)
        {
            try
            {
                var user = await _authentication.GetUserById(id);

                if (user == null)
                    return new BlitzWrapper<SingleUser>("Error", null, 404);

                var userMap = BlitzMapper.MapSourceToDestination(user);

                return new BlitzWrapper<SingleUser>("Result", userMap, 200);
            }
            catch (UnauthorizedAccessException ex)
            {
                return new BlitzWrapper<SingleUser>("Error" + ex.Message, null, 404);
            }
        }

        public async Task<BlitzWrapper<UserDetailsModel>> Login(UserLoginModel userLoginModel)
        {
            UserDetailsModel userDetails = null;
            User resultingUser = null;
            var responseObject = new BlitzWrapper<UserDetailsModel>();

            try
            {
                resultingUser = await _authentication.Login(userLoginModel.Email, userLoginModel.Password);
            }
            catch (Exception ex)
            {
                responseObject.ErrorMessage = ex.Message;
            }
            if (resultingUser != null)
            {
                userDetails = new UserDetailsModel
                {
                    UserId = resultingUser.Id,
                    Username = resultingUser.UserName,
                    Email = resultingUser.Email,
                    IsAdmin = resultingUser.IsAdmin,
                    JwtToken = TokenHelper.GenerateJwtToken(resultingUser.Email, resultingUser.Id, resultingUser.IsAdmin, _config)
                };
            }
            responseObject.ObjectResponse = userDetails;
            responseObject.StatusCode = 200;
            responseObject.ErrorMessage = "Succesfully login";

            return responseObject;
        }

        public async Task<BlitzWrapper<UserDetailsModel>> Signup(UserRegisterModel userRegisterModel)
        {
            BlitzWrapper<UserDetailsModel> response = new();
            User resultingUser = null;
            UserDetailsModel userDetails = null;

            try
            {
                resultingUser = await _authentication.Signup(userRegisterModel.Username, userRegisterModel.Password, userRegisterModel.Email);

                if (resultingUser != null)
                {
                    userDetails = new()
                    {
                        UserId = resultingUser.Id,
                        Email = resultingUser.Email,
                        Username = resultingUser.UserName,
                        JwtToken = TokenHelper.GenerateJwtToken(resultingUser.Email, resultingUser.Id, resultingUser.IsAdmin, _config)
                    };
                }
            }
            catch (InvalidOperationException ex)
            {
                response.ErrorMessage = ex.Message;
                response.StatusCode = 404;
                response.ObjectResponse = null;
                return response;
            }

            response.ObjectResponse = userDetails;
            response.ErrorMessage = "Succesfully login!";
            response.StatusCode = 200;
            return response;
        }
    }
}
