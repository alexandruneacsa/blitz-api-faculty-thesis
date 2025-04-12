using Newtonsoft.Json;
namespace Blitz.Application.Dtos
{
    public class VideoAttribute
    {
        public string Slug { get; set; }
        public string Description { get; set; }

        [JsonProperty("Created_At")]
        public DateTime CreatedAt { get; set; }
    }
}
