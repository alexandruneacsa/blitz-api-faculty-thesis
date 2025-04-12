namespace Blitz.Application.Dtos
{
    public class CreateDocumentResponse
    {
        public string Path { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}