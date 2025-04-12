using Blitz.Application.Dtos;
using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Blitz.API.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PexelsDotNetSDK.Models;

namespace Blitz.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/images")]
    public class ImageController : Controller
    {
        public readonly IServiceImage _imageService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public ImageController(IServiceImage serviceImage, IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
            _imageService = serviceImage;
        }

        /// <summary>
        /// Download pictures for a specific category. E.g. nature
        /// </summary>
        /// <param name="imageView"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ImageView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("download-all", Name = RouteNames.DownloadPicturesRoute)]
        public async Task<IActionResult> DownloadAllPictures([FromQuery] string query, CancellationToken cancellationToken)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            await AuthorityHelper.VerifyAdministratorAuthority(accessToken, _configuration, _authService);

            var response = await _imageService.GetImagesAsync(query, cancellationToken);
            var zipName = $"ImageFiles-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            return File(response.ToArray(), "application/zip", zipName);
        }

        /// <summary>
        /// Read all images from an external API, from Pexels. E.g. nature
        /// </summary>
        /// <param name="filteredPayload"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PhotoPage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = RouteNames.ReadImagesExternalRoute)]
        public async Task<IActionResult> GetNatureImages([FromQuery] FilteredPayload payload) =>
             Ok(await _imageService.FetchImagesExternal(payload.Query, payload.Orientation, payload.Size, payload.Color, payload.Locale, payload.Page, payload.PageSize));
    }
}
