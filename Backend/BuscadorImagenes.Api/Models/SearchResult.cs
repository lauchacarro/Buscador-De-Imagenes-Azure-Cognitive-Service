namespace BuscadorImagenes.Api.Models
{
    public class SearchResult
    {
        public long? Count { get; set; }
        public List<ImagenMultidioma> Results { get; set; }
    }
}
