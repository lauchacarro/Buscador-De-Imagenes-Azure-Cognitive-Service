namespace BuscadorImagenes.Api.Services
{
    using Azure;
    using Azure.Search.Documents;
    using Azure.Search.Documents.Models;

    using BuscadorImagenes.Api.Models;

    using Microsoft.AspNetCore.Mvc.Filters;

    using System;
    using System.Threading.Tasks;


    public class AzureSearchService
    {
        private readonly SearchClient _searchClient;

        public AzureSearchService()
        {

            _searchClient = CreateSearchClient();
        }

        private static SearchClient CreateSearchClient()
        {
            string searchServiceName = "https://buscador-imagenes-test-search.search.windows.net";
            string apiKey = null;
            string indexName = "imagenes-multidioma-index";

            SearchClient searchClient = new SearchClient(new Uri(searchServiceName), indexName, new AzureKeyCredential(apiKey));
            return searchClient;
        }


        public async Task CreateIndexDocumentAsync(ImagenMultidioma imagen)
        {
            IndexDocumentsBatch<ImagenMultidioma> batch = IndexDocumentsBatch.Create(
                IndexDocumentsAction.Upload(imagen));

            IndexDocumentsResult result = await _searchClient.IndexDocumentsAsync(batch);
        }

        public async Task<SearchResult> SearchAsync(string search, int skip, int size, IEnumerable<Models.FilterItem> filters)
        {

            SearchOptions options = new SearchOptions();
            options.Skip = skip;
            options.Size = size;
            options.Facets.Add("etiquetas_es");
            options.Facets.Add("etiquetas_en");
            options.Facets.Add("etiquetas_fr");
            options.Facets.Add("objetos_es");
            options.Facets.Add("objetos_en");
            options.Facets.Add("objetos_fr");

            options.Filter = CreateFilterExpression(filters.ToList(), ReadFacets());

            options.IncludeTotalCount = true;

            var response = await _searchClient.SearchAsync<ImagenMultidioma>(search, options);


            List<ImagenMultidioma> imagenes = new();

            await foreach (var item in response.Value.GetResultsAsync())
            {
                imagenes.Add(item.Document);
            }

            SearchResult result = new();
            result.Results = imagenes;
            result.Count = response.Value.TotalCount;

            Dictionary<string, IList<FacetItem>> facets = new();

            foreach (var facet in response.Value.Facets)
            {
                var key = facet.Key;

                foreach (var item in facet.Value)
                {
                    if (facets.ContainsKey(key))
                    {
                        facets[key].Add(new FacetItem
                        {
                            Value = item.Value.ToString(),
                            Count = item.Count.GetValueOrDefault()
                        });
                    }
                    else
                    {
                        facets.Add(key, new List<FacetItem>
                        {
                            new FacetItem
                            {
                                Value = item.Value.ToString(),
                                Count = item.Count.GetValueOrDefault()
                            }

                        });
                    }
                }
            }

            result.Facets = facets;


            return result;
        }

        static string CreateFilterExpression(List<Models.FilterItem> filterList, Dictionary<string, string> facets)
        {
            int i = 0;
            List<string> filterExpressions = new List<string>();

            while (i < filterList.Count)
            {
                string field = filterList[i].Field;
                string value = filterList[i].Value;

                if (facets.ContainsKey(field) && facets[field] == "array")
                {
                    filterExpressions.Add($"{field}/any(t: search.in(t, '{value}', ','))");
                }
                else
                {
                    filterExpressions.Add($"{field} eq '{value}'");
                }
                i += 1;
            }

            return string.Join(" and ", filterExpressions);
        }

        static Dictionary<string, string> ReadFacets()
        {
            string facetString = "etiquetas_es*,etiquetas_en*,etiquetas_fr*,objetos_es*,objetos_en*,objetos_fr*";

            string[] facets = facetString.Split(',');
            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (string f in facets)
            {
                if (f.Contains("*"))
                {
                    output[f.Replace("*", "")] = "array";
                }
                else
                {
                    output[f] = "string";
                }
            }

            return output;
        }


        public async Task<List<string>> AutocompleteAsync(string search)
        {

            AutocompleteOptions options = new AutocompleteOptions();


            options.Size = 5;
            options.UseFuzzyMatching = true;
            options.Mode = AutocompleteMode.OneTermWithContext;
            options.MinimumCoverage = 1;

            // Only one suggester can be specified per index.
            // The suggester for the Iamges index enables autocomplete/suggestions on the Tags field only.
            // During indexing, HotelNames are indexed in patterns that support autocomplete and suggested results.
            var suggestResult = await _searchClient.AutocompleteAsync(search, "sg", options).ConfigureAwait(false);

            // Convert the suggest query results to a list that can be displayed in the client.
            List<string> suggestions = suggestResult.Value.Results.Select(x => x.Text).ToList();


            return suggestions;
        }

        public async Task<List<string>> SuggestAsync(string search)
        {
            SuggestOptions options = new SuggestOptions();

            options.SearchFields.Add("etiquetas_es");
            options.SearchFields.Add("objetos_es");
            options.SearchFields.Add("palabras_es");
            options.Size = 5;
            options.UseFuzzyMatching = true;



            var suggestResult = await _searchClient.SuggestAsync<ImagenMultidioma>(search, "sg", options).ConfigureAwait(false);

            // Convert the suggest query results to a list that can be displayed in the client.
            List<string> suggestions = suggestResult.Value.Results.Select(x => x.Text).ToList();


            return suggestions;
        }

        public async Task<ImagenMultidioma> GetImagenAsync(Guid id)
        {
            var imagen = await _searchClient.GetDocumentAsync<ImagenMultidioma>(id.ToString());

            return imagen.Value;
        }

        public async Task DeleteImagenAsync(Guid id)
        {
            var imagen = await GetImagenAsync(id);

            await _searchClient.DeleteDocumentsAsync<ImagenMultidioma>(new[] { imagen });
        }
    }

}
