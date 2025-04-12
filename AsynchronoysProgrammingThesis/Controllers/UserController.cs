using Blitz.API.Configuration;
using Blitz.Application.Dtos;
using Blitz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blitz.API.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService) => _authService = authService;

        /// <summary>
        /// Provides access to resources authorizing a user with the right roles and permissions
        /// </summary>
        /// <param name="userLoginModel"></param>
        /// <returns></returns>
        [Route("api/login", Name = RouteNames.LoginRoute)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            try
            {
                var userToLogin = await _authService.Login(userLoginModel);

                if (userToLogin.ObjectResponse == null)
                    return BadRequest(userToLogin);

                return Ok(userToLogin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Used for adding a new user in the system
        /// </summary>
        /// <param name="userRegisterModel"></param>
        /// <returns></returns>
        [Route("api/signup", Name = RouteNames.SignupRoute)]
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] UserRegisterModel userRegisterModel)
        {
            try
            {
                var userTemp = await _authService.Signup(userRegisterModel);

                if (userTemp.ObjectResponse == null)
                {
                    return BadRequest(userTemp);
                }

                return Ok(userTemp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}