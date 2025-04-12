using Newtonsoft.Json;

namespace Blitz.Application.Dtos
{
    public class Video
    {
        [JsonProperty("Download_Link")]
        public string DownloadLink { get; set; }
    }
}
