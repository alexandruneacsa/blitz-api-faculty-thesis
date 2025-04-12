using PexelsDotNetSDK.Models;

namespace Blitz.Application.Interfaces
{
    public interface IServiceImage
    {
        Task<MemoryStream> GetImagesAsync(string query, CancellationToken cancellationToken);

        Task<PhotoPage> FetchImagesExternal(string query, string orientation = "", string size = "", string color = "",
            string locale = "", int page = 1, int pageSize = 15);
    }
}