namespace Blitz.Application.Dtos
{
    public class VideoData
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Video Video { get; set; }
        public List<VideoAttribute> Attributes { get; set; }
    }
}