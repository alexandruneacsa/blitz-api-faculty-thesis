using Blitz.Application.Dtos;
using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Blitz.API.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Blitz.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/video")]
    public class EmailController : Controller
    {
        public readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public EmailController(IEmailService emailService, IConfiguration configuration, IAuthService authService)
        {
            _emailService = emailService;
            _configuration = configuration;
            _authService = authService;
        }

        /// <summary>
        /// Used for sending concurrent emails to multiple recipients with an attachment
        /// </summary>
        /// <param name="emailPayload"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("send-email", Name = RouteNames.SendEmailsRoute)]
        public async Task<IActionResult> SendConcurrentEmail([FromForm] EmailPayload emailPayload)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            await AuthorityHelper.VerifyAdministratorAuthority(accessToken, _configuration, _authService);

            if (emailPayload.EmailAddresses == null || !emailPayload.EmailAddresses.Any())
            {
                return BadRequest();
            }

            await _emailService.SendEmailsAsync(emailPayload);

            return Ok();
        }
    }
}
