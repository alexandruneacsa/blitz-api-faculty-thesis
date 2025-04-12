using Blitz.Domain.Entities;
using Blitz.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blitz.Infrastructure.Repositories
{
    public class UserRepository : IAuthentication
    {
        private readonly BlitzContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(BlitzContext ctx, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _context = ctx;
            _signInManager = signInManager;
        }

        public async Task<User> Login(string email, string password)
        {
            User authUser = await _userManager.FindByEmailAsync(email);
            var passwordValidator = new PasswordValidator<User>();
            var result = await passwordValidator.ValidateAsync(_userManager, authUser, password);

            if (result.Succeeded)
            {
                return authUser;
            }
            else
            {
                throw new Exception("Utilizatorul nu s-a putut loga");
            }
        }

        public async Task<User> Signup(string username, string password, string email)
        {
            User userToBeRegistered = new User(email, username);
            IdentityResult result = null;
            try
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                }
                result = await _userManager.CreateAsync(userToBeRegistered, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            if (result != IdentityResult.Success)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName" || error.Code == "DuplicateEmail")
                    {
                        throw new InvalidOperationException("Could not be inserted");
                    }
                }
                throw new InvalidOperationException("Exception: " + result.Errors);
            }
            return userToBeRegistered;
        }

        public async Task<User> GetUserById(string userId) => await _context.Users
              .FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<User> GetUserByEmail(string email) => await _context.Users
                 .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> GetUserByUsername(string username) => await _context.Users
                 .FirstOrDefaultAsync(u => u.UserName == username);
    }
}
