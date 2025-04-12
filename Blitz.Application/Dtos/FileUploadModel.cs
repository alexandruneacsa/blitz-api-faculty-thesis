using Microsoft.AspNetCore.Http;

namespace Blitz.Application.Dtos
{
    public class FileUpload
    {
        public List<FileModel> FileModels { get; set; }
    }

    public class FileModel
    {
        public IFormFile FileDetails { get; set; }
        public string FileType { get; set; }
    }
}