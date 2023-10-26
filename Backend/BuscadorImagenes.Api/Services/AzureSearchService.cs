namespace BuscadorImagenes.Api.Services
{
    using Azure;
    using Azure.Search.Documents;
    using Azure.Search.Documents.Models;

    using BuscadorImagenes.Api.Models;

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
            string indexName = "imagenes-multidioma-index-test1";

            SearchClient searchClient = new SearchClient(new Uri(searchServiceName), indexName, new AzureKeyCredential(apiKey));
            return searchClient;
        }


        public async Task CreateIndexDocumentAsync(ImagenMultidioma imagen)
        {
            IndexDocumentsBatch<ImagenMultidioma> batch = IndexDocumentsBatch.Create(
                IndexDocumentsAction.Upload(imagen));

            IndexDocumentsResult result = await _searchClient.IndexDocumentsAsync(batch);
        }

        public async Task<SearchResult> SearchAsync(string search, int skip, int size)
        {

            SearchOptions options = new SearchOptions();
            options.Skip = skip;
            options.Size = size;

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


            return result;
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
