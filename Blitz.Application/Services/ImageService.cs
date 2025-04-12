using Blitz.Application.Helpers;
using Blitz.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System.IO.Compression;

namespace Blitz.Application.Services
{
    public class ImageService: IServiceImage
    {
        public readonly IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<MemoryStream> GetImagesAsync(string query, CancellationToken cancellationToken)
        {
            var data = await FetchImagesExternal(query);

            using MemoryStream ms = new();
            var tasksList = new List<Task>();

            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                foreach (var task in data.photos)
                {
                    var entry = zip.CreateEntry(Guid.NewGuid().ToString() + ".png");
                    var photoUrl = task.source.original;
                    var bytes = await ApiHelper.GetByteFromUrl(photoUrl);
                    using var entryStream = entry.Open();

                    tasksList.Add(entryStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken));
                }
                await Task.WhenAll(tasksList);
            }

            return ms;
        }

        public async Task<PhotoPage> FetchImagesExternal(string query, string orientation = "", string size = "", string color = "", string locale = "", int page = 1, int pageSize = 15)
        {
            var pexelsClient = new PexelsClient(_configuration["PexelsClientToken"]);
            var result = await pexelsClient.SearchPhotosAsync(query, orientation, size, color, locale, page, pageSize);

            return result;
        }
    }
}
