using Blitz.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Blitz.Application.Helpers
{
    public static class AuthorityHelper
    {
        public static async Task VerifyAdministratorAuthority(string accessToken, IConfiguration configuration, IAuthService authService)
        {
            var userId = TokenHelper.GetIdFromJwtToken(accessToken, configuration);
            var isAdmin = false;

            if (userId != null)
                isAdmin = (await authService.GetUserById(userId))!.ObjectResponse!.IsAdmin;
            if (isAdmin == false)
                throw new UnauthorizedAccessException();
        }
    }
}
