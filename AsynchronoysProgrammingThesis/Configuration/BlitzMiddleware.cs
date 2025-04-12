using Blitz.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Blitz.API.Configuration
{
    public class BlitzMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly IConfiguration _config;

        public BlitzMiddleware(RequestDelegate nextDelegate, IConfiguration config)
        {
            _nextDelegate = nextDelegate;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAuthService authenticationService)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"];
            var bearerToken = authorizationHeader.FirstOrDefault()?.Split(" ").Last();

            if (bearerToken != null)
            {
                await AttachUserToContext(httpContext, bearerToken, authenticationService);
            }

            await _nextDelegate(httpContext);
        }

        private async Task AttachUserToContext(HttpContext httpContext, string jwtToken,
            IAuthService authenticationService)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            jwtHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
            var token = (JwtSecurityToken)validatedToken;

            var emailClaim = token.Claims.First(claim => claim.Type == "email");
            var userEmail = emailClaim.Value;

            var user = await authenticationService.GetUserByEmail(userEmail);
            httpContext.Items["User"] = user;
        }
    }
}