using Blitz.API.Configuration;
using Blitz.Application.Dtos;
using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PexelsDotNetSDK.Models;

namespace Blitz.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/video")]
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public VideoController(IVideoService videoService, IConfiguration configuration, IAuthService authService)
        {
            _videoService = videoService;
            _configuration = configuration;
            _authService = authService;
        }

        /// <summary>
        /// Download videos for a specific category (nature, cars, programming and more)
        /// </summary>
        /// <param name="imageView"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ImageView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("download-all", Name = RouteNames.DownloadVideosRoute)]
        public async Task<IActionResult> DownloadAllVideos([FromQuery] string query, [FromQuery] string fileName,
            CancellationToken cancellationToken)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            await AuthorityHelper.VerifyAdministratorAuthority(accessToken, _configuration, _authService);

            var response = await _videoService.GetVideosAsync(query, cancellationToken);
            var zipName = $"{fileName}-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";

            return File(response.ToArray(), "application/zip", zipName);
        }

        /// <summary>
        /// Read all videos from an external API, from Pexels external API (nature, cars, programming and more)
        /// </summary>
        /// <param name="filteredPayload"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(VideoPage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = RouteNames.ReadVideosExternalRoute)]
        public async Task<IActionResult> GetFilteredPagedVideos([FromQuery] FilteredPayload payload) =>
            Ok(await _videoService.FetchVideosExternal(payload.Query, payload.Orientation, payload.Size, payload.Locale,
                payload.Page, payload.PageSize));


        /// <summary>
        /// Upload a new video into the database 
        /// </summary>
        /// <param name="createDocumentResponse"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateDocumentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("upload", Name = RouteNames.UploadVideosExternalRoute)]
        public async Task<IActionResult> UploadVideos([FromForm] List<IFormFile> formFiles)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            await AuthorityHelper.VerifyAdministratorAuthority(accessToken, _configuration, _authService);

            if (formFiles == null || !formFiles.Any())
                return BadRequest();

            var videos = await _videoService.CreateVideosAsync(formFiles);

            return Created("api/video/upload-successfull", videos);
        }
    }
}