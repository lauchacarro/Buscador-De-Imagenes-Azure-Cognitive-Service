using Azure.Search.Documents.Models;

using System.Collections.Generic;

namespace BuscadorImagenes.Api.Models
{
    public class SearchResult
    {
        public long? Count { get; set; }
        public List<ImagenMultidioma> Results { get; set; }
        public IDictionary<string, IList<FacetItem>> Facets { get; set; }
    }
}
