namespace Blitz.Application.Dtos
{
    public class VideoView
    {
        public int Id { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public byte[] ContentByte { get; set; }
    }
}