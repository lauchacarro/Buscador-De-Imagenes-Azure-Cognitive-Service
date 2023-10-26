using BuscadorImagenes.Api.Services;

using Microsoft.AspNetCore.Mvc;

namespace BuscadorImagenes.Api.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly AzureBlobStorageService _blobStorageService;
        private readonly AzureComputerVisionService _computerVisionService;
        private readonly AzureSearchService _searchService;
        private readonly AzureOpenAIService _openAIService;

        public ImagesController(
            AzureBlobStorageService blobStorageService,
            AzureComputerVisionService computerVisionService,
            AzureSearchService searchService,
            AzureOpenAIService openAIService)
        {
            _blobStorageService = blobStorageService;
            _computerVisionService = computerVisionService;
            _searchService = searchService;
            _openAIService = openAIService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Invalid file");
            }

            // Guarda la imagen en Azure Blob Storage
            var imageUrl = await _blobStorageService.UploadImageAsync(file);


            // Analiza la imagen con Azure Computer Vision
            var imagen = await _computerVisionService.AnalyzeImageAsync(imageUrl);


            var imagenMultidioma = await _openAIService.GetImagenMultidiomaAsync(imagen);

            imagenMultidioma.Id = Guid.NewGuid();
            imagenMultidioma.Url = imageUrl.ToString();
            imagenMultidioma.FechaCarga = DateTimeOffset.Now;


            // Guarda los resultados en Azure Search
            await _searchService.CreateIndexDocumentAsync(imagenMultidioma);

            return Ok(imagenMultidioma);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var imagen = await _searchService.GetImagenAsync(id);


            return Ok(imagen);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string search, int skip, int size)
        {
            var imagenes = await _searchService.SearchAsync(search, skip, size);


            return Ok(imagenes);
        }

        [HttpGet("Suggest")]
        public async Task<IActionResult> Suggest(string search)
        {
            var response = await _searchService.SuggestAsync(search);


            return Ok(response);
        }

        [HttpGet("Autocomplete")]
        public async Task<IActionResult> Autocomplete(string search)
        {
            var response = await _searchService.AutocompleteAsync(search);


            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _searchService.DeleteImagenAsync(id);


            return Ok();
        }
    }

}
