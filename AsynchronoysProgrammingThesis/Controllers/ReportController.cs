using Blitz.API.Configuration;
using Blitz.Application.Dtos;
using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Blitz.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/document")]
    public class ReportController : Controller
    {
        private readonly IReportService _documentService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public ReportController(IReportService documentService, IConfiguration configuration, IAuthService authService)
        {
            _documentService = documentService;
            _configuration = configuration;
            _authService = authService;
        }

        /// <summary>
        /// This endpoint is used to process the content of a big csv file or multiple ones.
        /// This method will read the csv lines, convert into a model and insert them into table in a parallel manner
        /// </summary>
        /// <param name="createDocumentResponse"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateDocumentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestSizeLimit(100_000_000)]
        [HttpPost("load", Name = RouteNames.LoadDocumentsRoute)]
        public async Task<IActionResult> LoadDocuments([FromForm] List<IFormFile> formFiles, CancellationToken cancellationToken)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if(string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            await AuthorityHelper.VerifyAdministratorAuthority(accessToken, _configuration, _authService);

            if (formFiles == null || !formFiles.Any())
                return BadRequest();

            var borrowersList = await _documentService.LoadBorrowersAsync(formFiles, cancellationToken);

            return Created("/api/document/load-successfull", borrowersList);
        }
    }
}
