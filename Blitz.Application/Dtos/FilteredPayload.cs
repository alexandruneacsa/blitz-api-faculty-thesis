namespace Blitz.Application.Dtos
{
    public class FilteredPayload
    {
        public string Query { get; set; }
        public string Orientation { get; set; } = "";
        public string Size { get; set; } = "";
        public string Locale { get; set; } = "";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public string Color { get; set; } = "";
    }
}
