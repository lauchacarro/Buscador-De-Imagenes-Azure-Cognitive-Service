namespace BuscadorImagenes.Api.Models
{
    public class SearchRequest
    {
        public string Search { get; set; } = "*";
        public int Skip { get; set; } = 0;
        public int Size { get; set; } = 8;
        public List<FilterItem> Filters { get; set; } = new();
    }
}

