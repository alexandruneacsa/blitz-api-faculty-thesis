using Microsoft.AspNetCore.Http;

namespace Blitz.Application.Dtos
{
    public class CreatePicturePayload
    {
        public IFormFile FileData { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
    }
}
