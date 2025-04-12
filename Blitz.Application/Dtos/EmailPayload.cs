using Microsoft.AspNetCore.Http;

namespace Blitz.Application.Dtos
{
    public class EmailPayload
    {
        public List<string> EmailAddresses { get; set; }
        public IFormFile Attachment { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
