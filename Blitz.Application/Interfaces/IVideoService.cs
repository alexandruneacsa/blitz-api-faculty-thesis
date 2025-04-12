using Blitz.Application.Dtos;
using Microsoft.AspNetCore.Http;
using PexelsDotNetSDK.Models;

namespace Blitz.Application.Interfaces
{
    public interface IVideoService
    {
        Task<MemoryStream> GetVideosAsync(string query, CancellationToken cancellationToken);

        Task<VideoPage> FetchVideosExternal(string query, string orientation = "", string size = "", string locale = "",
            int page = 1, int pageSize = 15);

        Task<BlitzWrapper<List<VideoView>>> CreateVideosAsync(List<IFormFile> formFiles);
    }
}